using AeroFly.Controllers.Models;

namespace AeroFly.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPaymentSync();
        Task<Payment> GetPaymentByIdAsync(int id);

        Task<Payment> UpdatePaymentAsync(Payment payment);

        Task<Payment> DeletePaymentAsync(int id);

        Task<Payment> AddPaymentAsync(Payment payment);
    }
}
