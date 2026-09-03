using MediatR;

namespace ECommerce.Application.Carts.Commands.AddItemsToCart
{
    public class AddingProductsToCartCommand : IRequest
    {
        public int ProductId { get; set; }
        public int CartItemId { get; set; }
        public int ShoppingCartId { get; set; }
        public int ItemQuantity { get; set; }
        public int CustomerId { get; set; }

    }
}
