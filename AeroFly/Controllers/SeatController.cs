using AeroFly.Controllers.Models;
using AeroFly.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace AeroFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatRepository _seatRepository;

        public SeatController(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seat>>> GetAllSeats()
        {
            var seats = await _seatRepository.GetAllSeatAsync();
            return Ok(seats);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Seat>> GetSeatById(int id)
        {
            var seat = await _seatRepository.GetSeatByIdAsync(id);
            return Ok(seat);
        }
        [HttpPost]
        public async Task<ActionResult<Seat>> AddSeat(Seat seat)
        {
            await _seatRepository.AddSeatAsync(seat);
            return CreatedAtAction(nameof(GetSeatById), new { id = seat.Id },seat);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Seat>> UpdateSeat(int id, Seat seat)
        {
            var se = await _seatRepository.GetSeatByIdAsync(id);
            if(se == null)
            {
                return NotFound();
            }
            await _seatRepository.UpdateSeatAsync(seat);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSeat(int id)
        {
            var delse = await _seatRepository.GetSeatByIdAsync(id);
            if(delse == null)
                return NotFound();
            await _seatRepository.DeleteSeatAsync(id);
            return NoContent();
        }
        [HttpGet("flight/{flightId}")]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeatsByFlightId(int flightId)
        {
            var seats = await _seatRepository.GetSeatsByFlightIdAsync(flightId);

            if (seats == null || !seats.Any())
            {
                return NotFound("No seats found for this flight.");
            }

            return Ok(seats);
        }

    }
}
