using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Customers.Queries.GetById
{
    public class GetByIdQuery : IRequest<Customer?>
    {
        public int id;
    }
}
