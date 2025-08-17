using AeroFly.Controllers.Models;
using AeroFly.DTOs.Booking;
using AeroFly.DTOs.Flight;
using AeroFly.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AeroFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetAllBookingAsync()
        {
            var bookings = await _bookingRepository.GetAllBookingAsync();
            var dtoList = bookings.Select(MapToBookingDTO);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDTO>> GetBookingById(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();
            return Ok(MapToBookingDTO(booking));
        }

        [HttpPost]
        public async Task<ActionResult<BookingDTO>> AddBookingAsync([FromBody] BookingCreateDTO dto)
        {
            var booking = new Booking
            {
                UserId = dto.UserId,
                FlightId = dto.FlightId,
                TotalAmount = dto.TotalAmount,
                BookingDate = DateTime.UtcNow,
                Status = "Confirmed"
            };

            var createdBooking = await _bookingRepository.AddBookingAsync(booking);
            var bookingWithFlight = await _bookingRepository.GetBookingByIdAsync(createdBooking.Id);

            return CreatedAtAction(nameof(GetBookingById), new { id = bookingWithFlight.Id }, MapToBookingDTO(bookingWithFlight));
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBookingsByUserId(int userId)
        {
            var bookings = await _bookingRepository.GetBookingsByUserId(userId);
            if (bookings == null || !bookings.Any())
                return NotFound("No bookings found for the user.");

            var dtoList = bookings.Select(MapToBookingDTO);
            return Ok(dtoList);
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();

            booking.Status = "Cancelled";
            await _bookingRepository.UpdateBookingAsync(booking);
            return NoContent();
        }

        // ✅ Helper method
        private BookingDTO MapToBookingDTO(Booking b)
        {
            return new BookingDTO
            {
                Id = b.Id,
                BookingDate = b.BookingDate,
                TotalAmount = b.TotalAmount,
                Status = b.Status,
                Flight = b.Flight == null ? null : new FlightDto
                {
                    FlightNumber = b.Flight.FlightNumber,
                    Origin = b.Flight.Origin,
                    Destination = b.Flight.Destination,
                    DepartureTime = b.Flight.DepartureTime,
                    ArrivalTime = b.Flight.ArrivalTime,
                    Price = b.Flight.Fare
                }
            };
        }
    }
}
