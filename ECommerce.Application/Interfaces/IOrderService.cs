using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> GetOrderById(int id);
        Task<List<Order>?> GetCustomerOrders(int customerId);
        Task CancelOrder(int id);

    }
}
