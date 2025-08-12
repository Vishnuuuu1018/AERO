using System.ComponentModel.DataAnnotations;

namespace AeroFly.Controllers.Models
{
    public class Payment
    {
        [Required]
        public int Id { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        [Required]
        public string PaymentMode { get; set; } 
        public string TransactionId { get; set; }

        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }

}
