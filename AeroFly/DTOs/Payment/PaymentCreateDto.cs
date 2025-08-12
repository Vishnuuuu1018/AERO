using System.ComponentModel.DataAnnotations;

namespace AeroFly.DTOs.Payment
{
    public class PaymentCreateDTO
    {
        [Required]
        public decimal AmountPaid { get; set; }

        [Required]
        public string PaymentMode { get; set; }  // e.g., "UPI", "Credit Card", etc.

        [Required]
        public string TransactionId { get; set; }

        [Required]
        public int BookingId { get; set; }
    }
}
