namespace ECommerce.Domain.Entities;

public class CartItems
{
    public int ProductId { get; set; }
    public int CartItemId { get; set; }
    public int ShoppingCartId { get; set; }
    public int ItemQuantity { get; set; }

    public ShoppingCart ShoppingCart { get; set; }

    public Product Product { get; set; }
}
