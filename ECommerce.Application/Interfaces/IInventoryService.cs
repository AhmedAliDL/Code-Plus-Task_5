using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IInventoryService
    {
        Task RestoreStockAsync(
           List<OrderItem> items);
    }
}
