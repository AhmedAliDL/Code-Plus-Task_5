using ECommerce.Application.Interfaces;
using StackExchange.Redis;

namespace ECommerce.DAL.Service
{
    public class ProductViewService : IProductViewService
    {
        private readonly IConnectionMultiplexer _redis;

        public ProductViewService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        public async Task TrackViewAsync(
        int productId,
        CancellationToken cancellationToken = default)
        {
            var database = _redis.GetDatabase();

            var now = DateTime.UtcNow;

            var periodStart = GetPeriodStart(now);

            var key =
                $"product:views:{periodStart:yyyy-MM-dd-HH}:{productId}";

            await database.StringIncrementAsync(key);

            await database.KeyExpireAsync(
                key,
                TimeSpan.FromDays(2));
        }

        private static DateTime GetPeriodStart(DateTime dateTime)
        {
            var hour = dateTime.Hour < 12 ? 0 : 12;

            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hour,
                0,
                0,
                DateTimeKind.Utc);
        }
    }
}
