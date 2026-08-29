namespace ECommerce.Application.Orders.Dtos
{
    public class CheckoutResponse
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal TotalAmount { get; set; }

        public string TransactionRef { get; set; }
    }
}
