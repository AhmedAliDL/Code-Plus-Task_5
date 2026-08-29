using ECommerce.DAL.Context;

public class CartItemsRepository : ICartItemsRepository
{
    private readonly AppDbContext _context;

    public CartItemsRepository(AppDbContext context)
    {
        _context = context;
    }


}