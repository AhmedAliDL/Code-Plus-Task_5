using ECommerce.Application.Customers.Commands.CreateCustomer;
using ECommerce.Application.Customers.Commands.UpgradeToVip;
using ECommerce.Application.Customers.Queries.GetById;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetById(GetByIdQuery request)
    {
        Customer? customer = await _mediator.Send(request);

        if (customer is not null)
            return NotFound($"Customer with ID {customer.Id} not found.");

        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> Create([FromBody] CreateCustomerCommand request)
    {
        try
        {
            var customer = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("{id}/upgrade-vip")]
    public async Task<IActionResult> UpgradeToVip(UpgradeToVipCommand request)
    {
        try
        {
            await _mediator.Send(request);
            return Ok(new { message = "Customer upgraded to VIP successfully." });
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (NotSupportedException ex)
        {
            return BadRequest(ex.Message);

        }

    }
}
