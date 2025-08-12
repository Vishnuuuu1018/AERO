using System.ComponentModel.DataAnnotations;

namespace AeroFly.Controllers.Models
{
    public class Seat
    {
        
        public int Id { get; set; }
        [Required]
        public string SeatNumber { get; set; }
        public bool IsBooked { get; set; } = false;
        [Required]

        public int FlightId { get; set; }
        public Flight? Flight { get; set; }

        public int? BookingId { get; set; }
        public Booking? Booking { get; set; }
    }

}
