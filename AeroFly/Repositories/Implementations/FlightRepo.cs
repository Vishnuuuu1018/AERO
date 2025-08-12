using AeroFly.Controllers.Models;
using AeroFly.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace AeroFly.Repositories.Implementations
{
    public class FlightRepo : IFlightRepository
    {
        private readonly AppDbContext _appDbContext;

        public FlightRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _appDbContext.Flights.ToListAsync();
        }
        public async Task<Flight?> GetFlightByIdAsync(int id)

        {
            return await _appDbContext.Flights.FindAsync(id);
        }

        public async Task<Flight> AddFlightAsync(Flight flight)
        {
            await _appDbContext.Flights.AddAsync(flight);
            await _appDbContext.SaveChangesAsync();
            return flight;
        }

        public async Task<Flight?> UpdateFlightAsync(Flight flight)
        {
            var existingFlight = await _appDbContext.Flights.FindAsync(flight.Id);
            if (existingFlight != null)
            {
                existingFlight.FlightName = flight.FlightName;
                existingFlight.FlightNumber = flight.FlightNumber;
                existingFlight.ArrivalTime = flight.ArrivalTime;
                existingFlight.DepartureTime = flight.DepartureTime;
                existingFlight.Origin = flight.Origin;
                existingFlight.Destination = flight.Destination;
                existingFlight.Fare = flight.Fare;
                existingFlight.Owner = flight.Owner;
                existingFlight.BaggageCheckInKg = flight.BaggageCheckInKg;

                await _appDbContext.SaveChangesAsync();
                return existingFlight;
            }
            return null;
        }
        public async Task<Flight> DeleteFlightAsync(int id)
        {

            var delFlight = await _appDbContext.Flights.FindAsync(id);
            if (delFlight != null)
            {

                _appDbContext.Flights.Remove(delFlight);
                await _appDbContext.SaveChangesAsync();
                return delFlight;
            }
            return null;
            
        }
    }

       


        
    }
