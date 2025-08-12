using AeroFly.Controllers.Models;
using AeroFly.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AeroFly.DTOs.Booking;
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
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookingAsync()
        {
            var bookings = await _bookingRepository.GetAllBookingAsync();
            return Ok(bookings);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            var bookings = await _bookingRepository.GetBookingByIdAsync(id);
            return Ok(bookings);
        }
        [HttpPost]
        public async Task<ActionResult<Booking>> AddBookingAsync([FromBody] BookingCreateDTO dto)
        {
            var booking = new Booking
            {
                UserId = dto.UserId,
                FlightId = dto.FlightId,
                TotalAmount = dto.TotalAmount,
                BookingDate = DateTime.UtcNow,
                Status = "Confirmed"
            };

            await _bookingRepository.AddBookingAsync(booking);
            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
        }

        [HttpPut]
        public async Task<ActionResult<Booking>> UpdateBookingAsync(int id,Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }
            await _bookingRepository.UpdateBookingAsync(booking);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Booking>> DeleteBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if(booking == null)
                return BadRequest();
            await _bookingRepository.DeleteBookingAsync(id);
            return NoContent();
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBookingsByUserId(int userId)
        {
            var bookings = await _bookingRepository.GetBookingsByUserId(userId);
            if (bookings == null || !bookings.Any())
                return NotFound("No bookings found for the user.");

            return Ok(bookings);
        }

    }
}
