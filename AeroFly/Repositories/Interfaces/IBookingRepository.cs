using AeroFly.Controllers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AeroFly.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllBookingAsync();

        Task<Booking?> GetBookingByIdAsync(int id);
        Task<Booking> AddBookingAsync(Booking booking);
        Task<Booking?> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int id);
        Task<IEnumerable<Booking>> GetBookingsByUserId(int userId);

    }
}
