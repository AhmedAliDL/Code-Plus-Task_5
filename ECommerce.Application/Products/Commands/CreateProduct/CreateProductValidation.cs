using ECommerce.Application.Interfaces;
using FluentValidation;

namespace ECommerce.Application.Products.Commands.CreateProduct
{
    public class CreateProductValidation : AbstractValidator<CreateProductCommand>
    {
        private readonly IProductRepo _productRepo;
        public CreateProductValidation(IProductRepo productRepo)
        {
            _productRepo = productRepo;
            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("Product price must be greater than zero.");
            RuleFor(p => p.StockQuantity)
                .GreaterThan(0)
                .WithMessage("Stock quantity cannot be negative.");
            RuleFor(p => p.SKU)
                .MustAsync(async (sku, CancellationToken) =>
                {
                    return await _productRepo.SKUExists(sku);
                })
                .WithMessage($"Product with this sku already exists.");

        }
    }
}
