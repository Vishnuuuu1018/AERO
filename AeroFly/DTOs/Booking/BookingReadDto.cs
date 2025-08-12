namespace AeroFly.DTOs.Booking
{
    public class BookingReadDTO
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        public int UserId { get; set; }
        public int FlightId { get; set; }

        public List<string> SeatNumbers { get; set; }  
    }
}
