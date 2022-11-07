using System.Text.Json;
using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.Abstractions.Token;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Facebook;
using ECommerce.Application.Exceptions;
using ECommerce.Domain.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Persistence.Services;

public class AuthService : IAuthService
{
    readonly HttpClient _httpClient;
    readonly IConfiguration _configuration;
    readonly UserManager<User> _userManager;
    readonly ITokenHandler _tokenHandler;
    readonly SignInManager<User> _signInManager;
    readonly IUserService _userService;

    public AuthService(IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        UserManager<User> userManager,
        ITokenHandler tokenHandler,
        SignInManager<User> signInManager,
        IUserService userService)
    {
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _signInManager = signInManager;
        _userService = userService;
    }

    async Task<Token> CreateUserExternalAsync(User user, string email, string name, UserLoginInfo info,
        int accessTokenLifeTime)
    {
        bool result = user != null;
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    UserName = email,
                    NameSurname = name
                };
                var identityResult = await _userManager.CreateAsync(user);
                result = identityResult.Succeeded;
            }
        }

        if (result)
        {
            await _userManager.AddLoginAsync(user, info); //AspNetUserLogins

            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 900);
            return token;
        }

        throw new Exception("Invalid external authentication.");
    }

    public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
    {
        string accessTokenResponse = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");

        FacebookAccessTokenResponse? facebookAccessTokenResponse =
            JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);

        string userAccessTokenValidation = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

        FacebookUserAccessTokenValidation? validation =
            JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);
        if (validation?.Data.IsValid != null)
        {
            string userInfoResponse =
                await _httpClient.GetStringAsync(
                    $"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

            FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

            var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
            Domain.Entities.Identity.User user =
                await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateUserExternalAsync(user, userInfo.Email, userInfo.Name, info, accessTokenLifeTime);
        }

        throw new Exception("Invalid external authentication.");
    }

    public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

        var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
        User user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        return await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTime);
    }

    public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
    {
        User user = await _userManager.FindByNameAsync(usernameOrEmail);
        if (user == null)
            user = await _userManager.FindByEmailAsync(usernameOrEmail);

        if (user == null)
            throw new NotFoundUserException();

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (result.Succeeded) //Authentication başarılı!
        {
            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 1200);
            return token;
        }

        throw new AuthenticationErrorException();
    }

    public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
    {
        User? user = await _userManager.Users.FirstOrDefaultAsync(c => c.RefreshToken == refreshToken);
        if (user is not null && user?.RefreshTokenEndDate > DateTime.UtcNow)
        {
            Token token = _tokenHandler.CreateAccessToken(900, user);
            await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 1200);
            return token;
        }
        else
            throw new NotFoundUserException();
    }
}