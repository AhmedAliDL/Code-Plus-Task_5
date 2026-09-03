using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services
{
    public class CartItemService : ICartItemsService
    {
        //get total amount of order
        public double CalculateCartItemsTotalAmount(List<CartItems> cartItems)
        {
            double totalAmount = 0;
            foreach (var item in cartItems)
            {
                totalAmount += item.ItemQuantity;
            }
            return totalAmount;
        }
    }
}
