using AeroFly.Controllers.Models;
using AeroFly.DTOs.Flight;
using AeroFly.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightRepository _flightRepository;

        public FlightController(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetAllFlights()
        {
            var flights = await _flightRepository.GetAllFlightsAsync();
            return Ok(flights);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlightById(int id)
        {
            var flight = await _flightRepository.GetFlightByIdAsync(id);
            if (flight == null) return NotFound();
            return Ok(flight);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Flight>> AddFlight([FromBody] FlightCreateDTO flightDto)
        {
            var flight = new Flight
            {
                FlightName = flightDto.FlightName,
                FlightNumber = flightDto.FlightNumber,
                Origin = flightDto.Origin,
                Destination = flightDto.Destination,
                DepartureTime = flightDto.DepartureTime,
                ArrivalTime = flightDto.ArrivalTime,
                TotalSeats = flightDto.TotalSeats,
                Fare = flightDto.Fare,
                BaggageCheckInKg = flightDto.BaggageCheckInKg,
                OwnerId = flightDto.OwnerId
            };

            await _flightRepository.AddFlightAsync(flight);
            return CreatedAtAction(nameof(GetFlightById), new { id = flight.Id }, flight);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(int id, [FromBody] FlightUpdateDto flightDto)
        {
            if (id != flightDto.Id) return BadRequest("Flight ID mismatch");

            var existing = await _flightRepository.GetFlightByIdAsync(id);
            if (existing == null) return NotFound();

            existing.FlightName = flightDto.FlightName;
            existing.FlightNumber = flightDto.FlightNumber;
            existing.Origin = flightDto.Origin;
            existing.Destination = flightDto.Destination;
            existing.DepartureTime = flightDto.DepartureTime;
            existing.ArrivalTime = flightDto.ArrivalTime;
            existing.TotalSeats = flightDto.TotalSeats;
            existing.Fare = flightDto.Fare;
            existing.BaggageCheckInKg = flightDto.BaggageCheckInKg;

            await _flightRepository.UpdateFlightAsync(existing);
            return Ok("Flight updated successfully");
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var flight = await _flightRepository.GetFlightByIdAsync(id);
            if (flight == null) return NotFound();

            await _flightRepository.DeleteFlightAsync(id);
            return Ok("Flight deleted successfully");
        }
    }
}
