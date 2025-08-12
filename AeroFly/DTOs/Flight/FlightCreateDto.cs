using System.ComponentModel.DataAnnotations;

namespace AeroFly.DTOs.Flight
{
    public class FlightCreateDTO
    {
        [Required]
        public string FlightName { get; set; }

        [Required]
        public string FlightNumber { get; set; }

        [Required]
        public string Origin { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public int TotalSeats { get; set; }

        [Required]
        public decimal Fare { get; set; }

        [Required]
        public int BaggageCheckInKg { get; set; }

        [Required]
        public int OwnerId { get; set; } // This maps to User.Id (Airline owner)
    }
}
