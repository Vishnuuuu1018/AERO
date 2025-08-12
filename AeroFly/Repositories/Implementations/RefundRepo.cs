using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AeroFly.Controllers.Models;

using AeroFly.Repositories;

namespace AeroFly.Repositories
{
    public class RefundRepo : IRefundRepository
    {
        private readonly AppDbContext _context;

        public RefundRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Refund>> GetAllRefundAsync()
        {
            return await _context.Refunds.Include(r => r.Booking).ToListAsync();
        }

        public async Task<Refund> GetRefundByIdAsync(int id)
        {
            return await _context.Refunds.FindAsync(id);
          
        }

        public async Task<Refund> AddRefundAsync(Refund refund)
        {
            _context.Refunds.Add(refund);
            await _context.SaveChangesAsync();
            return refund;
        }

        public async Task<Refund> UpdateRefundAsync(Refund refund)
        {
            var existingRefund = await _context.Refunds.FindAsync(refund.Id);
            if (existingRefund == null) return null;

            existingRefund.BookingId = refund.BookingId;
            existingRefund.RefundAmount = refund.RefundAmount;
            existingRefund.RefundDate = refund.RefundDate;
            existingRefund.RefundStatus = refund.RefundStatus;
            existingRefund.RefundReason = refund.RefundReason;
            existingRefund.ProcessedBy = refund.ProcessedBy;

            await _context.SaveChangesAsync();
            return existingRefund;
        }

        public async Task<bool> DeleteRefundAsync(int id)
        {
            var refund = await _context.Refunds.FindAsync(id);
            if (refund == null) return false;

            _context.Refunds.Remove(refund);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
