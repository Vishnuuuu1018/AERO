using System.ComponentModel.DataAnnotations;

namespace AeroFly.Controllers.Models
{
    public class Refund
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public decimal RefundAmount { get; set; }
        public DateTime RefundDate { get; set; } = DateTime.UtcNow;

        public string RefundStatus { get; set; } = "Pending"; 
        public string RefundReason { get; set; }
        public string ProcessedBy { get; set; } 
    }


}
