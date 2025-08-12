using System.ComponentModel.DataAnnotations;

namespace AeroFly.DTOs.Seat
{
    public class SeatCreateDTO
    {
        [Required]
        public string SeatNumber { get; set; }

        [Required]
        public int FlightId { get; set; }

        public int? BookingId { get; set; } 
    }
}
