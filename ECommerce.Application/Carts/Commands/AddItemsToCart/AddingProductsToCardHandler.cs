using ECommerce.Application.Events;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Carts.Commands.AddItemsToCart
{
    public class AddingProductsToCardHandler : IRequestHandler<AddingProductsToCartCommand>
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IProductRepo _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICartItemsRepository _cartItemRepository;
        private readonly IBackgroundTaskQueue<ProductAddedToCartEvent> _backgroundTaskQueue;
        public AddingProductsToCardHandler(ICustomerRepo customerRepo, IProductRepo productRepository, IShoppingCartRepository shoppingCartRepository, ICartItemsRepository cartItemRepository, IBackgroundTaskQueue<ProductAddedToCartEvent> backgroundTaskQueue)
        {
            _customerRepo = customerRepo;
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _cartItemRepository = cartItemRepository;
            _backgroundTaskQueue = backgroundTaskQueue;
        }
        public async Task Handle(AddingProductsToCartCommand request, CancellationToken cancellationToken)
        {

            Product? product = await _productRepository.GetProductById(request.ProductId);

            if (product == null)
                throw new Exception("Product not found.");
            if (product.StockQuantity > 0)
            {
                var customer = await _customerRepo.GetById(request.CustomerId);
                if (customer == null) throw new Exception("Customer not found.");

                var userCart = await _shoppingCartRepository.GetCartByCustomerId(customer!.Id);
                if (userCart == null)
                {
                    userCart = new ShoppingCart
                    {
                        CustomerId = request.CustomerId,

                    };
                    await _shoppingCartRepository.CreateCartAsync(userCart);
                }

                var userCartItemForProduct = await _cartItemRepository.GetCartItemForSameProductAsync(userCart!.CartId, request.ProductId);
                if (userCartItemForProduct == null)
                {
                    userCartItemForProduct = new CartItems
                    {

                        ShoppingCartId = userCart.CartId,
                        ProductId = request.ProductId
                    };
                    await _cartItemRepository.AddItemsAsync(userCartItemForProduct);
                    await _backgroundTaskQueue.QueueAsync(new ProductAddedToCartEvent
                    {
                        CustomerId = customer.Id,
                        ProductId = request.ProductId,
                        CartId = userCart.CartId,
                        Quantity = 1
                    });
                }
                else
                {
                    await _cartItemRepository.IncreaseCartItemQuantityAsync(userCart.CartId, request.ProductId, 1);
                }


            }
            else
                throw new Exception("Product out of stock");
        }
    }
}
