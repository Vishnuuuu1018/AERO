using AeroFly.Controllers.Models;
using AeroFly.DTOs.Flight;
using AeroFly.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            return Ok(flight);
        }
        [Authorize]
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
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Flight>> UpdateFlight(int id,Flight flight)
        {
            if (id != flight.Id)
            {
                return BadRequest();
            }
            await _flightRepository.UpdateFlightAsync(flight);
            return NoContent();  
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Flight>> DeleteFlight(int id)
        {
            var flight = await _flightRepository.GetFlightByIdAsync(id);
            if(flight == null)
            {
                return NotFound();
            }
            await _flightRepository.DeleteFlightAsync(id);
            return NoContent();
        }
       
    }
}
