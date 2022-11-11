using ECommerce.Application.Constansts;
using ECommerce.Application.CustomAttributes;
using ECommerce.Application.Enums;
using ECommerce.Application.Features.Commands.Order.CompleteOrder;
using ECommerce.Application.Features.Commands.Order.CreateOrder;
using ECommerce.Application.Features.Queries.Order.GetAllOrders;
using ECommerce.Application.Features.Queries.Order.GetByIdOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Orders,ActionType = ActionType.Writing,Definition = "Create Order")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
        {
            CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
            return Ok(response);
        }
        
        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Orders,ActionType = ActionType.Reading,Definition = "Get All Orders")]
        public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest getAllOrdersQueryRequest)
        {
            GetAllOrdersQueryResponse response = await _mediator.Send(getAllOrdersQueryRequest);
            return Ok(response);
        }
        
        [HttpGet("{id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Orders,ActionType = ActionType.Reading,Definition = "Get Order By Id")]
        public async Task<IActionResult> GetOrderById([FromRoute]GetByIdOrderQueryRequest getByIdOrderQueryRequest)
        {
            GetByIdOrderQueryResponse response = await _mediator.Send(getByIdOrderQueryRequest);
            return Ok(response);
        }
        
        [HttpGet("complete-order/{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Orders,ActionType = ActionType.Updating,Definition = "Complete Order")]
        public async Task<IActionResult> CompleteOrder([FromRoute] CompleteOrderCommandRequest completeOrderCommandRequest)
        {
            CompleteOrderCommandResponse response = await _mediator.Send(completeOrderCommandRequest);
            return Ok(response);
        }
    }
}
