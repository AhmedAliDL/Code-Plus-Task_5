
using ECommerce.Application.Interfaces;
using ECommerce.Application.Orders.Commands.Checkout;
using ECommerce.Domain.Entities;

namespace ECommerce.DAL.Repositories
{
    public class OrderItemsService : IOrderItemsService
    {
        private readonly IProductRepo _productRepo;
        public OrderItemsService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<List<OrderItem>> GetItemsOfCustomer(List<OrderItemRequestDto> items)
        {
            var orderItemsToSave = new List<OrderItem>();

            foreach (var itemDto in items)
            {
                var product = await _productRepo.GetProductById(itemDto.ProductId) ?? throw new Exception($"Product with ID {itemDto.ProductId} not found.");
                product.StockQuantity -= itemDto.Quantity;
                orderItemsToSave.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.Price
                });
            }
            return orderItemsToSave;
        }
    }
}
