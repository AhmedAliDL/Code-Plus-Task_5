using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
