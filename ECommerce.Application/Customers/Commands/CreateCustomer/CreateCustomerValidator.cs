using ECommerce.Application.Interfaces;
using FluentValidation;

namespace ECommerce.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        private readonly ICustomerRepo _customerRepo;
        public CreateCustomerValidator(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
            RuleFor(c => c.FullName)
                   .NotEmpty()
                   .WithMessage("Full name can`t be empty");
            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("Email can`t be empty")
                .EmailAddress()
                .WithMessage("email address is not valid")
                .MustAsync(async (email, CancellationToken) =>
                {
                    return await _customerRepo.EmailExists(email);
                })
                .WithMessage("Email is already registered.");

        }
    }
}
