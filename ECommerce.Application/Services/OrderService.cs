using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services
{
    public class OrderService : IOrderService
    {
        public Order CreateOrder(int customerId, decimal subtotal, decimal discount, decimal tax, decimal shipping, decimal netAmount, List<OrderItem> items)
        {
            return new Order
            {
                CustomerId = customerId,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Paid,
                Subtotal = subtotal,
                DiscountAmount = discount,
                TaxAmount = tax,
                ShippingFee = shipping,
                TotalAmount = netAmount,
                Items = items
            };
        }
    }
}
