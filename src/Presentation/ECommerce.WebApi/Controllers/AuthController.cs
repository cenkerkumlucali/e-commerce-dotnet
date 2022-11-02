using ECommerce.Application.Features.Commands.User.FacebookLogin;
using ECommerce.Application.Features.Commands.User.GoogleLogin;
using ECommerce.Application.Features.Commands.User.LoginUser;
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
}