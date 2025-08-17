namespace AeroFly.DTOs.Flight
{
    public class FlightUpdateDto
    {
        public int Id { get; set; }
        public string FlightName { get; set; }
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int TotalSeats { get; set; }
        public decimal Fare { get; set; }
        public int BaggageCheckInKg { get; set; }
    }
}

