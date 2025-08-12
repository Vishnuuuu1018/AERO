using AeroFly.Controllers.Models;
using AeroFly.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AeroFly.Repositories.Implementations
{
    public class SeatRepo : ISeatRepository
    {
        private readonly AppDbContext _context;

        public SeatRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Seat>> GetAllSeatAsync()
        {
            return await _context.Seats.ToListAsync();
        }
        public async Task<Seat?> GetSeatByIdAsync(int Id)
        {
            return await _context.Seats.FindAsync(Id);

        }
        public async Task<Seat> AddSeatAsync(Seat seat)
        {
            await _context.Seats.AddAsync(seat);
            await _context.SaveChangesAsync();
            return seat;
        }
        public async Task<Seat?> UpdateSeatAsync(Seat seat)
        {
            var existingseat = await _context.Seats.FindAsync(seat.Id);
            if (existingseat != null)
            {
                existingseat.SeatNumber = seat.SeatNumber;
                existingseat.IsBooked = seat.IsBooked;
                await _context.SaveChangesAsync();
                return existingseat;
            }
            return null;
        }

        public async Task DeleteSeatAsync(int id)
        {
            var delseat = await _context.Seats.FindAsync(id);
            if (delseat != null)
            {
                _context.Remove(delseat);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Seat>> GetSeatsByFlightIdAsync(int flightId)
        {
            return await _context.Seats
                        .Where(s => s.FlightId == flightId)
                        .ToListAsync();
        }
    }
}
