using ECommerce.Application.Interfaces;
using FluentValidation;

namespace ECommerce.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductValidation : AbstractValidator<UpdateProductCommand>
    {
        private readonly IProductRepo _productRepo;
        public UpdateProductValidation(IProductRepo productRepo)
        {
            _productRepo = productRepo;
            RuleFor(p => p.id)
                .MustAsync(async (productId, cancellationToken) =>
                {
                    return await _productRepo.GetProductById(productId) != null;
                })
                .WithMessage("Product not found.");
            RuleFor(p => p.product.Id)
                .GreaterThan(0)
                .WithMessage("Price must be positive.");
        }
    }

}
