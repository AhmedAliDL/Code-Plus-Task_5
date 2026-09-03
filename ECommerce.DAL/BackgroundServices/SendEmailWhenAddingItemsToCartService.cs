using ECommerce.Application.Events;
using ECommerce.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ECommerce.DAL.BackgroundServices
{
    public class SendEmailWhenAddingItemsToCartService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IBackgroundTaskQueue<ProductAddedToCartEvent>
        _queue;
        private readonly ILogger<SendEmailWhenAddingItemsToCartService> _logger;
        public SendEmailWhenAddingItemsToCartService(IServiceScopeFactory serviceScopeFactory, IBackgroundTaskQueue<ProductAddedToCartEvent> queue, ILogger<SendEmailWhenAddingItemsToCartService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _queue = queue;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(
               "Cart notification background service started.");
                try
                {
                    var cartEvent = await _queue.DequeueAsync(stoppingToken);
                    using var scope = _serviceScopeFactory.CreateScope();

                    var notifyService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                    await notifyService
                       .SendProductAddedToCartMessageAsync(
                           cartEvent.CustomerId,
                           cartEvent.ProductId,
                           cartEvent.Quantity,
                           stoppingToken);

                }
                catch (Exception ex)
                {
                    throw new Exception("There`s error happen when sending message");
                }
                _logger.LogInformation(
               "Cart notification background service stopped.");
            }
        }
    }
}
