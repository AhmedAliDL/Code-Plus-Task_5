using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepo _paymentRepo;
        public PaymentService(IPaymentRepo paymentRepo)
        {
            _paymentRepo = paymentRepo;
        }
        public async Task<string> PaymentProcessTransaction(Order order, decimal netAmount)
        {
            var txRef = $"TX-LEGACY-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
            var payment = new Payment
            {
                Order = order,
                Amount = netAmount,
                PaymentDate = DateTime.UtcNow,
                TransactionReference = txRef,
                IsSuccess = true
            };

            try
            {
                await _paymentRepo.ApplyPaymentTransaction(order, payment);
                return txRef;
            }
            catch
            {
                throw new Exception("An error occurred while saving the order.");
            }
        }
    }
}
