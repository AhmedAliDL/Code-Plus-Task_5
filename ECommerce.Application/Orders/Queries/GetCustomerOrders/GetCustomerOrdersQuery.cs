using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Orders.Queries.GetCustomerOrders
{
    public class GetCustomerOrdersQuery : IRequest<List<Order>?>
    {
        public int id;
    }

}
