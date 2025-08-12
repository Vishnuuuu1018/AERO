namespace AeroFly.DTOs.Refund
{
    public class RefundReadDTO
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public decimal RefundAmount { get; set; }
        public DateTime RefundDate { get; set; }
        public string RefundStatus { get; set; }
        public string RefundReason { get; set; }
        public string ProcessedBy { get; set; }
    }
}
