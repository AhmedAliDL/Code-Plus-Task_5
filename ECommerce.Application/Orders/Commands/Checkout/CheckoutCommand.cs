using ECommerce.Application.Orders.Dtos;
using MediatR;

namespace ECommerce.Application.Orders.Commands.Checkout
{
    public class OrderItemRequestDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class CheckoutCommand : IRequest<CheckoutResponse>
    {
        public int CustomerId { get; set; }
        public List<OrderItemRequestDto> Items { get; set; } = new();
        public string? CouponCode { get; set; }
    }
}
