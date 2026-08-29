using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface ICouponRepo
    {
        Task<Coupon?> GetCoupon(string couponCode);
        Task<decimal> ApplyCouponDiscount(decimal subtotal, string couponCode);
    }
}
