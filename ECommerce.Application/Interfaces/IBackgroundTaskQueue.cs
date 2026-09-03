namespace ECommerce.Application.Interfaces
{
    public interface IBackgroundTaskQueue<T>
    {
        ValueTask QueueAsync(T item);

        ValueTask<T> DequeueAsync(CancellationToken cancellationToken);
    }
}
