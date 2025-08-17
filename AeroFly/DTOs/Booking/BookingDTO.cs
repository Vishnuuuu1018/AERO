using AeroFly.DTOs.Flight;

namespace AeroFly.DTOs.Booking
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public FlightDto Flight { get; set; }
    }
}
