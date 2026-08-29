namespace ECommerce.Application.Interfaces
{
    public interface IDiscountService
    {
        Task<decimal> CalDiscount(decimal subtotal, string? couponCode, int customerId);
    }
}
