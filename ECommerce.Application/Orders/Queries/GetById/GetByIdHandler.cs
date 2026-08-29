using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Orders.Queries.GetById
{
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, Order?>
    {
        private readonly IOrderRepo _orderRepo;
        public GetByIdHandler(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public Task<Order?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            return _orderRepo.GetOrderByIdAsync(request.id);
        }
    }
}
