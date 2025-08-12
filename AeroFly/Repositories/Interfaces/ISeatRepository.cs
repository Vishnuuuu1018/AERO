using AeroFly.Controllers.Models;
namespace AeroFly.Repositories.Interfaces
{
    public interface ISeatRepository
    {
        Task<IEnumerable<Seat>> GetAllSeatAsync();
        Task<Seat?> GetSeatByIdAsync(int Id);
        Task<Seat> AddSeatAsync(Seat seat);

        Task<Seat?>UpdateSeatAsync(Seat seat);

        Task DeleteSeatAsync(int id);
        Task<IEnumerable<Seat>> GetSeatsByFlightIdAsync(int flightId);


    }
}
