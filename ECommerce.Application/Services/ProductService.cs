using ECommerce.Application.Interfaces;
using ECommerce.Application.Orders.Commands.Checkout;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;

        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<decimal> CalSubTotalOfProducts(List<OrderItemRequestDto> Items)
        {
            decimal subtotal = 0m;

            foreach (var itemDto in Items)
            {
                var product = await _productRepo.GetProductById(itemDto.ProductId) ?? throw new Exception($"Product with ID {itemDto.ProductId} not found.");

                if (product.StockQuantity < itemDto.Quantity)
                {
                    throw new Exception($"Insufficient stock for product '{product.Name}'. Available: {product.StockQuantity}, Requested: {itemDto.Quantity}");
                }

                subtotal += product.Price * itemDto.Quantity;
            }
            return subtotal;
        }
    }
}
