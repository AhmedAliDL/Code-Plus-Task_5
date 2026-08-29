using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Orders.Queries.GetById
{
    public class GetByIdQuery : IRequest<Order?>
    {
        public int id;
    }
}
