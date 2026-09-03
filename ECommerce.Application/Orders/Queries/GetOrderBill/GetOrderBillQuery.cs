using MediatR;

namespace ECommerce.Application.Orders.Queries.GetOrderBill
{
    public class GetOrderBillQuery : IRequest<string>
    {

    }
    public class GetOrderBillHandler : IRequestHandler<GetOrderBillQuery, string>
    {
        public Task<string> Handle(GetOrderBillQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
