using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Orders.Queries.GetCustomerOrders
{
    public class GetCustomerOrdersHandler : IRequestHandler<GetCustomerOrdersQuery, List<Order>?>
    {
        private readonly IOrderRepo _orderRepo;
        public GetCustomerOrdersHandler(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public Task<List<Order>?> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            return _orderRepo.GetOrdersByCustomerIdAsync(request.id);
        }
    }

}
