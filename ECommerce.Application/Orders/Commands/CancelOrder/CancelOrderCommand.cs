using MediatR;

namespace ECommerce.Application.Orders.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest
    {
        public int id;
    }
}
