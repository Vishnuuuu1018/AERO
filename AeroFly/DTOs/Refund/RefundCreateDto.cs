using System.ComponentModel.DataAnnotations;

namespace AeroFly.DTOs.Refund
{
    public class RefundCreateDTO
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public decimal RefundAmount { get; set; }

        public string RefundReason { get; set; }

        public string ProcessedBy { get; set; }  // Admin name (optional at creation)
    }
}
