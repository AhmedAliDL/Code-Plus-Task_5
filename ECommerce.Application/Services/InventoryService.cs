using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepo _productRepo;

        public InventoryService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task RestoreStockAsync(List<OrderItem> items)
        {
            foreach (var item in items)
            {
                await _productRepo.RestoreStockAsync(item.ProductId, item.Quantity);
            }
        }
    }
}
