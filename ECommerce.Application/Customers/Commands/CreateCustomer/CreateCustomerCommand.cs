using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsVip { get; set; }
    }
}
