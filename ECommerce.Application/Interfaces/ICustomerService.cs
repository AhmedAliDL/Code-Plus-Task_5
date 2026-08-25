using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> Create(CreateCustomerDto dto);
        Task<Customer?> GetById(int id);
        Task UpgradeToVip(int id);

    }
}
