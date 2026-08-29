using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Products.Commands.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductRepo _productRepo;
        public CreateProductHandler(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<Product> Handle(CreateProductCommand dto, CancellationToken cancellationToken)
        {

            var product = new Product
            {
                Name = dto.Name,
                SKU = dto.SKU.ToUpper(),
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };
            return await _productRepo.CreateProduct(product);
        }
    }
}
