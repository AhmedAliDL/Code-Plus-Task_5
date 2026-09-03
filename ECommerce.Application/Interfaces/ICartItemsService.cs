using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface ICartItemsService
    {
        double CalculateCartItemsTotalAmount(List<CartItems> cartItems);
    }
}
