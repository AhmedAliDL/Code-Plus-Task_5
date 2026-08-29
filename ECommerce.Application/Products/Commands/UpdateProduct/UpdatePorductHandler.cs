using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.Products.Commands.UpdateProduct
{
    public class UpdatePorductHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepo _productRepo;
        public UpdatePorductHandler(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existing = await _productRepo.GetProductById(request.id);
            existing.Name = request.product.Name;
            existing.SKU = request.product.SKU;
            existing.Price = request.product.Price;
            existing.StockQuantity = request.product.StockQuantity;
            await _productRepo.UpdateProduct(existing);

        }
    }

}
