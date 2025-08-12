using System.ComponentModel.DataAnnotations;

namespace AeroFly.Controllers.Models
{
    public class Booking
    {
        [Required]
        public int Id { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Confirmed";
        [Required]

        public int UserId { get; set; }
        public User User { get; set; }
        [Required]

        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public ICollection<Seat> Seats { get; set; }
        public Payment Payment { get; set; }
        public Refund Refund { get; set; }
    }

}
