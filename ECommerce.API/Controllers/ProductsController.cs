using ECommerce.Application.Products.Commands.CreateProduct;
using ECommerce.Application.Products.Commands.DeleteProduct;
using ECommerce.Application.Products.Commands.UpdateProduct;
using ECommerce.Application.Products.Queries.GeById;
using ECommerce.Application.Products.Queries.GetAllProducts;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll(GetAllProductsQuery request)
    {
        var products = await _mediator.Send(request);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(GetByIdQuery request)
    {
        var product = await _mediator.Send(request);
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] CreateProductCommand request)
    {
        try
        {
            var product = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"An error occurred while creating the product: {ex.Message}");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand request)
    {
        try
        {
            await _mediator.Send(request);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Product with ID {request.id} not found.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"An error occurred while updating the product: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(DeleteProductCommand request)
    {
        try
        {
            await _mediator.Send(request);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

    }
}
