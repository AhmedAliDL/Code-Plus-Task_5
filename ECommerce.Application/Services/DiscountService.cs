using ECommerce.Application.Interfaces;

namespace ECommerce.Application.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly ICouponRepo _couponRepo;
        public DiscountService(ICustomerRepo customerRepo,
            ICouponRepo couponRepo)
        {
            _customerRepo = customerRepo;
            _couponRepo = couponRepo;
        }
        private async Task<decimal> CalVipDiscount(decimal subtotal, int customerId)
        {
            return await _customerRepo.ApplyVipDiscount(subtotal, customerId);
        }
        private async Task<decimal> CalCouponDiscount(decimal subtotal, string? couponCode)
        {
            if (!string.IsNullOrWhiteSpace(couponCode))
            {
                decimal couponDiscount = await _couponRepo.ApplyCouponDiscount(subtotal, couponCode);
                if (couponDiscount == 0)
                {
                    throw new Exception($"Invalid or inactive coupon code '{couponCode}'.");
                }
                return couponDiscount;
            }
            return 0;
        }
        public async Task<decimal> CalDiscount(decimal subtotal, string? couponCode, int customerId)
        {
            return await CalCouponDiscount(subtotal, couponCode)
                + await CalVipDiscount(subtotal, customerId);
        }




    }
}
