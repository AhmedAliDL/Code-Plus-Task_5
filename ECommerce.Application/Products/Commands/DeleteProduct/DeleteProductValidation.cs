using ECommerce.Application.Interfaces;
using FluentValidation;

namespace ECommerce.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductValidation : AbstractValidator<DeleteProductCommand>
    {
        private readonly IProductRepo _productRepo;
        public DeleteProductValidation(IProductRepo productRepo)
        {
            _productRepo = productRepo;
            RuleFor(p => p.id)
                .MustAsync(async (productId, cancellationToken) =>
                {
                    return await _productRepo.GetProductById(productId) != null;
                })
                .WithMessage("Product not found.");
        }
    }
}
