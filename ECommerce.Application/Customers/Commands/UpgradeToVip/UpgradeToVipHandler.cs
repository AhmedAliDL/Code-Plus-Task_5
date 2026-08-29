using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.Customers.Commands.UpgradeToVip
{
    public class UpgradeToVipHandler : IRequestHandler<UpgradeToVipCommand>
    {
        private readonly ICustomerRepo _customerRepo;
        public UpgradeToVipHandler(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public async Task Handle(UpgradeToVipCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepo.GetById(request.id);
            var totalSpent = _customerRepo.GetTotalSpent(customer);
            if (totalSpent < 500m)
            {
                throw new NotSupportedException($"Customer does not qualify for VIP. Total spend {totalSpent:C} is less than required $500.00");
            }
            await _customerRepo.UpgradeToVip(customer);
        }
    }
}
