using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(int customerId, decimal subtotal, decimal discount, decimal tax, decimal shipping, decimal netAmount, List<OrderItem> items);

    }
}
