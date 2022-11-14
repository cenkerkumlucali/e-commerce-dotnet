using ECommerce.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;
using ECommerce.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("get-roles-to-endpoint")]
        public async Task<IActionResult> GetRolesToEndpoint([FromBody] GetRolesToEndpointsQueryRequest getRolesToEndpointsQueryRequest)
        {
            GetRolesToEndpointsQueryResponse? response = await _mediator.Send(getRolesToEndpointsQueryRequest);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRoleEndpoint([FromBody] AssignRoleEndpointCommandRequest assignRoleEndpointCommandRequest)
        {
            assignRoleEndpointCommandRequest.Type = typeof(Program);
            AssignRoleEndpointCommandResponse? response = await _mediator.Send(assignRoleEndpointCommandRequest);
            return Ok(response);
        }
    }
}