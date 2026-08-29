using MediatR;

namespace ECommerce.Application.Customers.Commands.UpgradeToVip
{

    public class UpgradeToVipCommand : IRequest
    {
        public int id;
    }
}
