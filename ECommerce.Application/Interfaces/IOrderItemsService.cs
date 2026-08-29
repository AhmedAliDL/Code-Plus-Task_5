using ECommerce.Application.Orders.Commands.Checkout;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IOrderItemsService
    {
        Task<List<OrderItem>> GetItemsOfCustomer(List<OrderItemRequestDto> items);
    }
}
