using ECommerce.DAL.Context;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class CartItemsRepository : ICartItemsRepository
{
    private readonly AppDbContext _context;

    public CartItemsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CartItems?> GetCartItemForSameProductAsync(int shoppingCartId, int productId)
    {
        return await _context.CartItems.FirstOrDefaultAsync(ci => ci.ShoppingCartId == shoppingCartId && ci.ProductId == productId);
    }
    public async Task AddItemsAsync(CartItems cartItems)
    {
        await _context.CartItems.AddAsync(cartItems);
        await _context.SaveChangesAsync();
    }
    public async Task IncreaseCartItemQuantityAsync(int shoppingCartId, int productId, int newQuantity)
    {
        CartItems? cartItem = await GetCartItemForSameProductAsync(shoppingCartId, productId);
        if (cartItem == null)
            return;
        cartItem.ItemQuantity += newQuantity;
        await _context.SaveChangesAsync();
    }
}