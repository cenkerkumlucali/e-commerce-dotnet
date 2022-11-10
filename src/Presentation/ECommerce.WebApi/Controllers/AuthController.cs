using ECommerce.Application.Features.Commands.User.FacebookLogin;
using ECommerce.Application.Features.Commands.User.GoogleLogin;
using ECommerce.Application.Features.Commands.User.LoginUser;
using ECommerce.Application.Features.Commands.User.PasswordReset;
using ECommerce.Application.Features.Commands.User.RefreshTokenLogin;
using ECommerce.Application.Features.Commands.User.VerifyResetToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
    {
        LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommandRequest refreshTokenCommandRequest)
    {
        RefreshTokenCommandResponse response = await _mediator.Send(refreshTokenCommandRequest);
        return Ok(response);
    }

    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
    {
        GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
        return Ok(response);
    }

    [HttpPost("facebook-login")]
    public async Task<IActionResult> FacebookLogin(FacebookLoginCommandRequest facebookLoginCommandRequest)
    {
        FacebookLoginCommandResponse response = await _mediator.Send(facebookLoginCommandRequest);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest passwordResetCommandRequest)
    {
        PasswordResetCommandResponse? response = await _mediator.Send(passwordResetCommandRequest);
        return Ok(response);
    }
    [HttpPost("verify-reset-token")]
    public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest verifyResetTokenCommandRequest)
    {
        VerifyResetTokenCommandResponse response = await _mediator.Send(verifyResetTokenCommandRequest);
        return Ok(response);
    }
}