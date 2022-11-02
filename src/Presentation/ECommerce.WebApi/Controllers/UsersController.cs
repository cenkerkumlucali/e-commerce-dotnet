using ECommerce.Application.Features.Commands.Product.CreateProduct;
using ECommerce.Application.Features.Commands.User.CreateUser;
using ECommerce.Application.Features.Commands.User.FacebookLogin;
using ECommerce.Application.Features.Commands.User.GoogleLogin;
using ECommerce.Application.Features.Commands.User.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
    }
}
