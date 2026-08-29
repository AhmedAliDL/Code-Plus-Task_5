using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Customers.Queries.GetById
{
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, Customer?>
    {
        private readonly ICustomerRepo _customerRepo;
        public GetByIdHandler(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public Task<Customer?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            return _customerRepo.GetById(request.id);
        }
    }
}
