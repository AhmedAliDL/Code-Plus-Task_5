using ECommerce.Application.Interfaces;
using ECommerce.DAL.Context;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly AppDbContext _context;

        public CustomerRepo(AppDbContext context)
        {

        }
        public async Task<Customer> Create(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Customers.AnyAsync(c => c.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));

        }

        public async Task<Customer?> GetById(int id)
        {
            return await _context.Customers
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public decimal GetTotalSpent(Customer customer)
        {
            return customer.Orders
            .Where(o => o.Status == OrderStatus.Paid)
            .Sum(o => o.TotalAmount);
        }

        public async Task UpgradeToVip(Customer customer)
        {
            customer.IsVip = true;
            await _context.SaveChangesAsync();
        }
    }
}
