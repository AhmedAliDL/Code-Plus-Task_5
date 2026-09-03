using ECommerce.DAL.Context;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly AppDbContext _context;

    public ShoppingCartRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddProductsToCart(ShoppingCart cart)
    {
        await _context.ShoppingCart.AddAsync(cart);
        await _context.SaveChangesAsync();
    }
    public async Task<ShoppingCart?> GetCartByCustomerId(int customerId)
    {
        return await _context.ShoppingCart.LastOrDefaultAsync(c => c.CustomerId == customerId);
    }
    public async Task CreateCartAsync(ShoppingCart cart)
    {
        _context.ShoppingCart.Add(cart);
        await _context.SaveChangesAsync();
    }

}