using ECommerce.Application.Interfaces;
using FluentValidation;

namespace ECommerce.Application.Customers.Commands.UpgradeToVip
{
    public class UpgradeToVipValidator : AbstractValidator<UpgradeToVipCommand>
    {
        private readonly ICustomerRepo _customerRepo;
        public UpgradeToVipValidator(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
            RuleFor(utv => utv.id)
                .NotEmpty()
                .WithMessage("Customer id can`t be empty")
                .MustAsync(async (customerId, CancellationToken) =>
                {
                    return await _customerRepo.GetById(customerId) != null;
                })
                .WithMessage($"Customer not found.");
        }
    }
}
