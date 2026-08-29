using ECommerce.Application.Interfaces;

namespace ECommerce.Application.Services
{
    public class OrderChargesService : IOrderChargesService
    {
        public decimal CalTax(decimal amount)
        {
            return Math.Round(amount * 0.14m, 2);
        }
        public decimal CalShipping(decimal amount)
        {
            return amount >= 1000m ? 0m : 75m;
        }
        public decimal CalOrderTotalAmount(decimal initialAmount, decimal discount, decimal tax, decimal shipping)
        {
            if (discount > initialAmount)
            {
                discount = initialAmount;
            }
            decimal totalAmount = initialAmount + tax + shipping - discount;
            if (totalAmount > 50000m)
            {
                throw new Exception("Payment processing failed. Amount exceeds limit.");
            }

            return totalAmount;
        }
    }
}
