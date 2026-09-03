using ECommerce.Domain.Entities;

public interface IShoppingCartRepository
{
    Task AddProductsToCart(ShoppingCart cart);
    Task<ShoppingCart?> GetCartByCustomerId(int customerId);
    Task CreateCartAsync(ShoppingCart cart);
}