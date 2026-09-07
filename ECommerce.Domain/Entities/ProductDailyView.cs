namespace ECommerce.Domain.Entities
{
    public class ProductDailyView
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        public DateTime PeriodStart { get; set; }

        public DateTime PeriodEnd { get; set; }

        public long ViewCount { get; set; }

        public Product Product { get; set; } = null!;
    }
}
