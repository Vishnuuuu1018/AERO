using AeroFly.Controllers.Models;
using AeroFly.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefundController : ControllerBase
    {
        private readonly IRefundRepository _refundRepository;

        public RefundController(IRefundRepository refundRepository)
        {
            _refundRepository = refundRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Refund>>> GetAll()
        {
            var refunds = await _refundRepository.GetAllRefundAsync();
            return Ok(refunds);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Refund>> GetById(int id)
        {
            var refund = await _refundRepository.GetRefundByIdAsync(id);
            if (refund == null)
                return NotFound();

            return Ok(refund);
        }

        [HttpPost]
        public async Task<ActionResult<Refund>> Create(Refund refund)
        {
            var createdRefund = await _refundRepository.AddRefundAsync(refund);
            return CreatedAtAction(nameof(GetById), new { id = createdRefund.Id }, createdRefund);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Refund>> Update(int id, Refund refund)
        {
            if (id != refund.Id) return BadRequest("ID mismatch.");

            var updatedRefund = await _refundRepository.UpdateRefundAsync(refund);
            if (updatedRefund == null) return NotFound();

            return Ok(updatedRefund);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _refundRepository.DeleteRefundAsync(id);
            if (!success) return NotFound();

            return NoContent();

        }
    }
}
