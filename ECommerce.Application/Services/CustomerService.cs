using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public async Task<Customer> Create(CreateCustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new ArgumentException("Full name is required.");

            if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
                throw new ArgumentException("A valid email address is required.");
            var emailExists = await _customerRepo.EmailExists(dto.Email);
            if (emailExists)
            {
                throw new ArgumentException("Email is already registered.");
            }
            var customer = new Customer
            {
                FullName = dto.FullName,
                Email = dto.Email,
                IsVip = dto.IsVip
            };
            return await _customerRepo.Create(customer);
        }

        public Task<Customer?> GetById(int id)
        {
            return _customerRepo.GetById(id);
        }

        public async Task UpgradeToVip(int id)
        {
            var customer = await _customerRepo.GetById(id) ?? throw new ArgumentException($"Customer with ID {id} not found.");
            var totalSpent = _customerRepo.GetTotalSpent(customer);
            if (totalSpent < 500m)
            {
                throw new NotSupportedException($"Customer does not qualify for VIP. Total spend {totalSpent:C} is less than required $500.00");
            }
            await _customerRepo.UpgradeToVip(customer);
        }
    }
}
