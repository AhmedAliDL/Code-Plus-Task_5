using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface ICustomerRepo
    {
        Task<Customer?> GetById(int id);
        Task<Customer> Create(Customer customer);
        Task UpgradeToVip(Customer customer);
        Task<bool> EmailExists(string email);
        decimal GetTotalSpent(Customer customer);
        Task<decimal> ApplyVipDiscount(decimal subtotal, int customerId);
    }
}
