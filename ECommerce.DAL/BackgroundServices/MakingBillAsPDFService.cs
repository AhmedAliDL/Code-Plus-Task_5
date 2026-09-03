using ECommerce.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerce.DAL.BackgroundServices
{
    public class MakingBillAsPDFService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public MakingBillAsPDFService(
            IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using IServiceScope scope =
                    _scopeFactory.CreateScope();

                var billService =
                    scope.ServiceProvider
                        .GetRequiredService<IBillService>();

                await billService.GenerateBillsAsync(
                    );

                await Task.Delay(
                    TimeSpan.FromMinutes(5),
                    stoppingToken);
            }
        }
    }
}
