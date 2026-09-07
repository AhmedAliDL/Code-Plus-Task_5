using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Products.Queries.GeById
{
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, Product?>
    {
        private readonly IProductRepo _productRepo;
        private readonly IProductViewService _productViewService;
        public GetByIdHandler(IProductRepo productRepo, IProductViewService productViewService)
        {
            _productRepo = productRepo;
            _productViewService = productViewService;
        }
        public async Task<Product?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            await _productViewService.TrackViewAsync(request.id, cancellationToken);
            return await _productRepo.GetProductById(request.id);
        }
    }

}
