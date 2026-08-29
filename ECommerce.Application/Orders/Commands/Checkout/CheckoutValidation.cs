using ECommerce.Application.Interfaces;
using FluentValidation;

namespace ECommerce.Application.Orders.Commands.Checkout
{
    public class CheckoutValidation : AbstractValidator<CheckoutCommand>
    {
        private readonly ICustomerRepo _customerRepo;
        public CheckoutValidation(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
            RuleFor(o => o.Items)
                .NotNull()
                .WithMessage("Cannot checkout an empty order.");
            RuleFor(o => o.Items.Count)
                .GreaterThan(0)
                .WithMessage("Cannot checkout an empty order.");

            RuleFor(o => o.CustomerId)
                .MustAsync(async (customerId, CancellationToken) =>
                {
                    return await _customerRepo.GetById(customerId) != null;
                })
                .WithMessage($"Customer not found.");

        }
    }
}
