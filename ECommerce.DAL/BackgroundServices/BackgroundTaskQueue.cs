using ECommerce.Application.Interfaces;
using System.Threading.Channels;

namespace ECommerce.DAL.BackgroundServices
{
    public class BackgroundTaskQueue<T> : IBackgroundTaskQueue<T>
    {
        private readonly Channel<T> _queue;
        public BackgroundTaskQueue()
        {
            _queue = Channel.CreateUnbounded<T>();
        }
        public async ValueTask QueueAsync(T item)
        {
            await _queue.Writer.WriteAsync(item);
        }
        public async ValueTask<T> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }

    }
}
