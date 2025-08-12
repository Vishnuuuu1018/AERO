using AeroFly.Controllers.Models;

using AeroFly.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AeroFly.Repositories
{
    public class BookingRepo : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingAsync()
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Flight)
                .Include(b => b.Seats)
                .Include(b => b.Payment)
                .Include(b => b.Refund)
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Flight)
                .Include(b => b.Seats)
                .Include(b => b.Payment)
                .Include(b => b.Refund)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> UpdateBookingAsync(Booking booking)
        {
            var existing = await _context.Bookings.FindAsync(booking.Id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(booking);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserId(int userId)
        {
            return await _context.Bookings
        .Where(b => b.UserId == userId)
        .Include(b => b.Flight)
        .Include(b => b.Seats)
        .Include(b=> b.Payment)
        .ToListAsync();
        }
    }
}
