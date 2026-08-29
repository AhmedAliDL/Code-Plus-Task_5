using ECommerce.Application.Orders.Commands.Checkout;

namespace ECommerce.Application.Interfaces
{
    public interface IProductService
    {
        Task<decimal> CalSubTotalOfProducts(List<OrderItemRequestDto> Items);
    }
}
