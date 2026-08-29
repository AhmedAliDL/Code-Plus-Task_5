using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<string> PaymentProcessTransaction(Order order, decimal netAmount);
    }
}
