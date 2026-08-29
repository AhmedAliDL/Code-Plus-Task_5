using ECommerce.Application.Interfaces;
using ECommerce.Application.Orders.Dtos;
using MediatR;

namespace ECommerce.Application.Orders.Commands.Checkout
{
    public class CheckoutHandler : IRequestHandler<CheckoutCommand, CheckoutResponse>
    {
        private readonly IOrderItemsService _orderItemsService;
        private readonly IProductService _productService;
        private readonly IDiscountService _discountService;
        private readonly IOrderChargesService _orderChargesService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;

        public CheckoutHandler(IOrderItemsService orderItemsService, IProductService productService, IDiscountService discountService, IPaymentService paymentService, IOrderChargesService orderChargesService, IOrderService orderService)
        {
            _orderItemsService = orderItemsService;
            _productService = productService;
            _discountService = discountService;
            _paymentService = paymentService;
            _orderChargesService = orderChargesService;
            _orderService = orderService;
        }
        public async Task<CheckoutResponse> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {

            decimal subtotal;
            subtotal = await _productService.CalSubTotalOfProducts(request.Items);


            var orderItemsToSave = await _orderItemsService.GetItemsOfCustomer(request.Items);

            decimal discount = await _discountService.CalDiscount(subtotal, request.CouponCode, request.CustomerId);

            var tax = _orderChargesService.CalTax(subtotal);
            var shipping = _orderChargesService.CalShipping(subtotal);
            var netAmount = _orderChargesService.CalOrderTotalAmount(
                subtotal,
                discount,
                tax,
                shipping);



            var order = _orderService.CreateOrder(request.CustomerId, subtotal, discount, tax, shipping, netAmount, orderItemsToSave);

            var tax_ref = await _paymentService.PaymentProcessTransaction(order, netAmount);


            return new CheckoutResponse
            {
                Id = order.Id,
                Status = order.Status.ToString(),
                Subtotal = subtotal,
                DiscountAmount = discount,
                TaxAmount = tax,
                ShippingFee = shipping,
                TotalAmount = netAmount,
                TransactionRef = tax_ref
            };
        }
    }
}
