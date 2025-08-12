namespace AeroFly.DTOs.Seat
{
    public class SeatReadDTO
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public bool IsBooked { get; set; }
        public int FlightId { get; set; }
        public int? BookingId { get; set; }
    }
}
