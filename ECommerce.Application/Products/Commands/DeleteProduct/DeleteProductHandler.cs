using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepo _productRepo;
        public DeleteProductHandler(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepo.GetProductById(request.id);
            await _productRepo.DeleteProduct(product);
        }
    }

}
