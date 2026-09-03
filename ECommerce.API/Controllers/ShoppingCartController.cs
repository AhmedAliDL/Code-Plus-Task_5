using ECommerce.Application.Carts.Commands.AddItemsToCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

public class ShoppingCartController(IMediator mediator) : BaseController(mediator)
{

    [HttpPost("addItemCart")]
    public async Task<IActionResult> AddItemToCart([FromBody] AddingProductsToCartCommand req)
    {
        try
        {
            await _mediator.Send(req);
            return Ok("Item has added.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
