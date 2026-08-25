using ECommerce.Application.Interfaces;
using ECommerce.DAL.Context;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDbContext _context;
        public OrderRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task CancelOrderAsync(Order order)
        {
            order.Status = OrderStatus.Cancelled;
            await _context.SaveChangesAsync();
        }
        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .Include(o => o.Payment)
            .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>?> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.Orders
           .Include(o => o.Items)
           .Where(o => o.CustomerId == customerId)
           .ToListAsync();
        }
    }
}
