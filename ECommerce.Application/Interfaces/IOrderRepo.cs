using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IOrderRepo
    {
        Task<Order?> GetOrderByIdAsync(int id);
        Task<List<Order>?> GetOrdersByCustomerIdAsync(int customerId);
        Task CancelOrderAsync(Order order);
    }
}
