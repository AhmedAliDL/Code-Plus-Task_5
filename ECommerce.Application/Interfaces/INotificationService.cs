namespace ECommerce.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendProductAddedToCartMessageAsync(
            int customerId,
            int productId,
            int quantity,
            CancellationToken cancellationToken);
    }
}
