using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IPaymentRepo
    {
        Task ApplyPaymentTransaction(Order order, Payment payment);
    }
}
