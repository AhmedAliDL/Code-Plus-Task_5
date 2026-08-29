using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<List<Product>?>
    {

    }
}
