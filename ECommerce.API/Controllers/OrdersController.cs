using ECommerce.Application.Orders.Commands.CancelOrder;
using ECommerce.Application.Orders.Commands.Checkout;
using ECommerce.Application.Orders.Queries.GetById;
using ECommerce.Application.Orders.Queries.GetCustomerOrders;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IMediator mediator) : BaseController(mediator)
{

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(GetByIdQuery request)
    {
        var order = await _mediator.Send(request);

        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<List<Order>>> GetCustomerOrders(GetCustomerOrdersQuery request)
    {
        var orders = await _mediator.Send(request);

        return Ok(orders);
    }

    [HttpPost("cancel/{id}")]
    public async Task<IActionResult> CancelOrder(CancelOrderCommand request)
    {

        try
        {
            await _mediator.Send(request);
            return Ok(new { message = "Order cancelled successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] CheckoutCommand request)
    {
        try
        {
            return Ok(await _mediator.Send(request));
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
