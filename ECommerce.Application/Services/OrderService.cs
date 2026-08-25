using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IInventoryService _inventoryService;
        public OrderService(IOrderRepo orderRepo, IInventoryService inventoryService)
        {
            _orderRepo = orderRepo;
            _inventoryService = inventoryService;
        }
        public async Task CancelOrder(int id)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id);
            if (order == null) throw new Exception("Order not found");
            if (order.Status == OrderStatus.Cancelled) throw new Exception("Order is already cancelled");
            if (order.Status == OrderStatus.Paid)
            {
                await _inventoryService.RestoreStockAsync(order.Items);
            }
            await _orderRepo.CancelOrderAsync(order);
        }

        public Task<List<Order>?> GetCustomerOrders(int customerId)
        {
            return _orderRepo.GetOrdersByCustomerIdAsync(customerId);
        }

        public Task<Order?> GetOrderById(int id)
        {
            return _orderRepo.GetOrderByIdAsync(id);
        }
    }
}
