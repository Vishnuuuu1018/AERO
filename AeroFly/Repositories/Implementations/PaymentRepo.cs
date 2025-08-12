using AeroFly.Controllers.Models;
using AeroFly.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AeroFly.Repositories.Implementations
{
    public class PaymentRepo : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Payment>> GetAllPaymentSync()
        {
            return await _context.Payment.ToListAsync();
        }
        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _context.Payment.FindAsync(id);
        }
        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            await  _context.Payment.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment> UpdatePaymentAsync(Payment payment)
        {
            var existingPay = await _context.Payment.FindAsync(payment.Id);
            if (existingPay != null)
            {
                existingPay.PaymentDate = payment.PaymentDate;
                existingPay.AmountPaid = payment.AmountPaid;
                existingPay.PaymentMode = payment.PaymentMode;

            }
            await _context.SaveChangesAsync();
            return existingPay;
        }

        public async Task<Payment> DeletePaymentAsync(int id)
        {
            var delpay = await _context.Payment.FindAsync(id);
            if(delpay != null)
            {
                _context.Remove(delpay);
                _context.SaveChanges();
            }
            return null;
        }

    

  

    }
}
