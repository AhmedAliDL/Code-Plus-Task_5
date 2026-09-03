namespace ECommerce.Domain.Entities;

public class ShoppingCart
{
    public int CartId { get; set; }
    public int CustomerId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public Customer Customer { get; set; }

    public List<CartItems>? ProductsCartItems { get; set; }
}