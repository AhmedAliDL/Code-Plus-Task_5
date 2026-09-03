using ECommerce.Domain.Entities;

public interface ICartItemsRepository
{
    Task<CartItems?> GetCartItemForSameProductAsync(int shoppingCartId, int productId);
    Task AddItemsAsync(CartItems cartItems);
    Task IncreaseCartItemQuantityAsync(int shoppingCartId, int productId, int newQuantity);
}