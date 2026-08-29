using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Products.Queries.GeById
{
    public class GetByIdQuery : IRequest<Product?>
    {
        public int id;
    }

}
