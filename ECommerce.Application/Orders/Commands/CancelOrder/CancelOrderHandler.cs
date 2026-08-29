using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Orders.Commands.CancelOrder
{
    public class CancelOrderHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IInventoryService _inventoryService;
        public CancelOrderHandler(IOrderRepo orderRepo, IInventoryService inventoryService)
        {
            _orderRepo = orderRepo;
            _inventoryService = inventoryService;
        }
        public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepo.GetOrderByIdAsync(request.id); ;
            if (order.Status == OrderStatus.Cancelled)
                throw new Exception("Order is already cancelled");
            if (order.Status == OrderStatus.Paid)
            {
                await _inventoryService.RestoreStockAsync(order.Items);
            }
            await _orderRepo.CancelOrderAsync(order);
        }
    }
}
