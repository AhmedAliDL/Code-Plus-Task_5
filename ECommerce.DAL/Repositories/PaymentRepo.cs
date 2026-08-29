using ECommerce.Application.Interfaces;
using ECommerce.DAL.Context;
using ECommerce.Domain.Entities;

namespace ECommerce.DAL.Repositories
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly AppDbContext _context;
        public PaymentRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task ApplyPaymentTransaction(Order order, Payment payment)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
