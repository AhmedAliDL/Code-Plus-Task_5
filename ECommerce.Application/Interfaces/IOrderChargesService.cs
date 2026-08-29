namespace ECommerce.Application.Interfaces
{
    public interface IOrderChargesService
    {
        decimal CalOrderTotalAmount(decimal initialAmount, decimal discount, decimal tax, decimal shipping);
        decimal CalTax(decimal amount);
        decimal CalShipping(decimal amount);
    }
}
