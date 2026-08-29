using MediatR;

namespace ECommerce.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public int id;
    }
}
