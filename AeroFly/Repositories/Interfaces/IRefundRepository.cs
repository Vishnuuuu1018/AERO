using System.Collections.Generic;
using System.Threading.Tasks;
using AeroFly.Controllers.Models;

namespace AeroFly.Repositories
{
    public interface IRefundRepository
    {
        Task<IEnumerable<Refund>> GetAllRefundAsync();
        Task<Refund> GetRefundByIdAsync(int id);
        Task<Refund> AddRefundAsync(Refund refund);
        Task<Refund> UpdateRefundAsync(Refund refund);
        Task<bool> DeleteRefundAsync(int id);
    }
}
