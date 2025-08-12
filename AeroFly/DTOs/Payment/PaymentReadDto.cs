namespace AeroFly.DTOs.Payment
{
    public class PaymentReadDTO
    {
        public int Id { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionId { get; set; }
        public int BookingId { get; set; }
    }
}
