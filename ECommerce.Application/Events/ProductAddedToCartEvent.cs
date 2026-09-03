namespace ECommerce.Application.Events
{
    public class ProductAddedToCartEvent
    {
        public int CustomerId { get; init; }
        public int ProductId { get; init; }
        public int CartId { get; init; }
        public int Quantity { get; init; }
    }
}
