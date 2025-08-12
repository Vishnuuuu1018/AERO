using System.ComponentModel.DataAnnotations;

namespace AeroFly.DTOs.Booking
{
    public class BookingCreateDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int FlightId { get; set; }


        [Required]
        public decimal TotalAmount { get; set; }  
    }
}
