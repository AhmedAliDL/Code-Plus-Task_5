namespace ECommerce.Application.Interfaces
{
    public interface IProductViewService
    {
        Task TrackViewAsync(
            int productId,
            CancellationToken cancellationToken = default);
    }
}
