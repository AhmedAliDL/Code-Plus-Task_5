using ECommerce.Application.Interfaces;
using FluentValidation;

namespace ECommerce.Application.Orders.Commands.CancelOrder
{
    public class CancelOrderValidation : AbstractValidator<CancelOrderCommand>
    {
        private readonly IOrderRepo _orderRepo;
        public CancelOrderValidation(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
            RuleFor(o => o.id)
                .NotEmpty()
                .WithMessage("Order id is required.")
                .MustAsync(async (orderId, cancellationToken) =>
                {
                    return await _orderRepo.GetOrderByIdAsync(orderId) != null;
                })
                .WithMessage("Order not found");
        }

    }
}
