using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Products.Queries.GeById
{
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, Product?>
    {
        private readonly IProductRepo _productRepo;
        public GetByIdHandler(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public Task<Product?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            return _productRepo.GetProductById(request.id);
        }
    }
   
}
