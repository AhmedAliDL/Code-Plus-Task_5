using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ICustomerRepo _customerRepo;
        public CreateCustomerHandler(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public async Task<Customer> Handle(CreateCustomerCommand dto, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                FullName = dto.FullName,
                Email = dto.Email,
                IsVip = dto.IsVip
            };
            return await _customerRepo.Create(customer);
        }
    }
}
