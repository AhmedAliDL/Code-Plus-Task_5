using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public int id;
        public Product product;
    }

}
