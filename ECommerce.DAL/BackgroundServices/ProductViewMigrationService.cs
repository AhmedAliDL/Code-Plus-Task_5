using ECommerce.DAL.Context;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace ECommerce.DAL.BackgroundServices
{
    public class ProductViewMigrationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConnectionMultiplexer _redis;
        private readonly ILogger<ProductViewMigrationService> _logger;

        public ProductViewMigrationService(
            IServiceScopeFactory scopeFactory,
            IConnectionMultiplexer redis,
            ILogger<ProductViewMigrationService> logger)
        {
            _scopeFactory = scopeFactory;
            _redis = redis;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Product View Migration Service started.");

            // Migrate any old buckets first.
            await MigrateExpiredBucketsAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var delay = GetDelayUntilNext12HourPeriod();

                    _logger.LogInformation(
                        "Next product view migration in {Delay}.",
                        delay);

                    await Task.Delay(
                        delay,
                        stoppingToken);

                    await MigrateExpiredBucketsAsync(
                        stoppingToken);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Error occurred while migrating product views.");

                    await Task.Delay(
                        TimeSpan.FromMinutes(1),
                        stoppingToken);
                }
            }

            _logger.LogInformation(
                "Product View Migration Service stopped.");
        }

        private async Task MigrateExpiredBucketsAsync(
            CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            var currentPeriodStart =
                GetPeriodStart(now);

            _logger.LogInformation(
                "Looking for product view buckets before {CurrentPeriod}.",
                currentPeriodStart);

            var database = _redis.GetDatabase();

            var server = _redis.GetServer(
                _redis.GetEndPoints().First());

            const string pattern =
                "product:views:*";

            var keys = server.Keys(
                pattern: pattern);

            using var scope =
                _scopeFactory.CreateScope();

            var dbContext =
                scope.ServiceProvider
                    .GetRequiredService<AppDbContext>();

            foreach (var key in keys)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var periodStart =
                    ExtractPeriodStart(key);

                if (periodStart == null)
                    continue;

                if (periodStart >= currentPeriodStart)
                    continue;

                await MigrateBucketAsync(
                    database,
                    dbContext,
                    key,
                    periodStart.Value,
                    cancellationToken);
            }
        }

        private async Task MigrateBucketAsync(
            IDatabase redis,
            AppDbContext dbContext,
            RedisKey key,
            DateTime periodStart,
            CancellationToken cancellationToken)
        {
            var value =
                await redis.StringGetAsync(key);

            if (!value.HasValue)
                return;

            if (!long.TryParse(
                    value.ToString(),
                    out var viewCount))
            {
                _logger.LogWarning(
                    "Invalid Redis value for key {Key}.",
                    key);

                return;
            }

            if (viewCount <= 0)
            {
                await redis.KeyDeleteAsync(key);
                return;
            }

            var periodEnd =
                periodStart.AddHours(12);

            var productId =
                ExtractProductId(key);

            if (productId <= 0)
            {
                _logger.LogWarning(
                    "Could not extract ProductId from key {Key}.",
                    key);

                return;
            }

            var existing =
                await dbContext.ProductDailyViews
                    .SingleOrDefaultAsync(
                        x =>
                            x.ProductId == productId &&
                            x.PeriodStart == periodStart,
                        cancellationToken);

            if (existing == null)
            {
                dbContext.ProductDailyViews.Add(
                    new ProductDailyView
                    {
                        ProductId = productId,
                        PeriodStart = periodStart,
                        PeriodEnd = periodEnd,
                        ViewCount = viewCount,
                    });
            }
            else
            {
                existing.ViewCount = viewCount;
            }

            await dbContext.SaveChangesAsync(
                cancellationToken);

            await redis.KeyDeleteAsync(key);

            _logger.LogInformation(
                "Migrated {ViewCount} views for Product {ProductId}. " +
                "Period: {PeriodStart} - {PeriodEnd}.",
                viewCount,
                productId,
                periodStart,
                periodEnd);
        }

        private static DateTime GetPeriodStart(
            DateTime dateTime)
        {
            var hour = dateTime.Hour < 12
                ? 0
                : 12;

            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hour,
                0,
                0,
                DateTimeKind.Utc);
        }

        private static TimeSpan GetDelayUntilNext12HourPeriod()
        {
            var now = DateTime.UtcNow;

            DateTime nextPeriod;

            if (now.Hour < 12)
            {
                nextPeriod = new DateTime(
                    now.Year,
                    now.Month,
                    now.Day,
                    12,
                    0,
                    0,
                    DateTimeKind.Utc);
            }
            else
            {
                nextPeriod = new DateTime(
                    now.Year,
                    now.Month,
                    now.Day,
                    0,
                    0,
                    0,
                    DateTimeKind.Utc)
                    .AddDays(1);
            }

            return nextPeriod - now;
        }

        private static DateTime? ExtractPeriodStart(
            RedisKey key)
        {

            var parts =
                key.ToString().Split(':');

            if (parts.Length != 4)
                return null;

            var period =
                parts[2];

            if (!DateTime.TryParseExact(
                    period,
                    "yyyy-MM-dd-HH",
                    null,
                    System.Globalization.DateTimeStyles.AssumeUniversal |
                    System.Globalization.DateTimeStyles.AdjustToUniversal,
                    out var result))
            {
                return null;
            }

            return result;
        }

        private static int ExtractProductId(
            RedisKey key)
        {
            var parts =
                key.ToString().Split(':');

            if (parts.Length != 4)
                return 0;

            return int.TryParse(
                parts[3],
                out var productId)
                ? productId
                : 0;
        }
    }
}
