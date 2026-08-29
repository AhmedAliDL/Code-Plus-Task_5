using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>?>
    {
        private readonly IProductRepo _productRepo;
        public GetAllProductsHandler(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public Task<List<Product>?> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return _productRepo.GetAllProducts();
        }
    }
}
