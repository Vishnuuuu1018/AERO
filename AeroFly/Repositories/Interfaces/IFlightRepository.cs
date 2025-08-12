using AeroFly.Controllers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AeroFly.Repositories.Interfaces
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        Task<Flight?> GetFlightByIdAsync(int id);
        Task<Flight> AddFlightAsync(Flight flight);
        Task<Flight?> UpdateFlightAsync(Flight flight);
        Task<Flight?> DeleteFlightAsync(int id);
    }
}
