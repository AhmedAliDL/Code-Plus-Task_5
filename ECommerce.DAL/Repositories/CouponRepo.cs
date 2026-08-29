using ECommerce.Application.Interfaces;
using ECommerce.DAL.Context;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL.Repositories
{
    public class CouponRepo : ICouponRepo
    {
        private readonly AppDbContext _context;
        public CouponRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Coupon?> GetCoupon(string couponCode)
        {
            return await _context.Coupons
                    .FirstOrDefaultAsync(c => c.Code.Equals(couponCode) && c.IsActive);
        }
        public async Task<decimal> ApplyCouponDiscount(decimal subtotal, string couponCode)
        {
            var coupon = await GetCoupon(couponCode);

            if (coupon != null)
            {
                return Math.Round(subtotal * (coupon.DiscountPercentage / 100m), 2);
            }
            return 0;
        }
    }
}
