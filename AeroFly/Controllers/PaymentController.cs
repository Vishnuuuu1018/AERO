using AeroFly.Controllers.Models;
using AeroFly.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _payment;

        public PaymentController(IPaymentRepository payment)
        {
            _payment = payment;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAllAsync()
        {
            var payments = await _payment.GetAllPaymentSync();
            return Ok(payments);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPaymentByIdAsync(int id )
        {
            var payment = await _payment.GetPaymentByIdAsync(id);
            return Ok(payment);
        }
        [HttpPost]
        public async Task<ActionResult<Payment>> AddPaymentAsync(Payment payment)
        {
            await _payment.AddPaymentAsync(payment);
            return Ok(payment);
        }
        [HttpPut]
        public async Task<ActionResult<Payment>> UpdatePaymentAsync(int id, Payment payment)
        {
            if (id != payment.Id)
            {
                return BadRequest();
            }
            await _payment.UpdatePaymentAsync(payment);
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Payment>> DeletePaymentAsync(int id)
        {
            var pay = await _payment.GetPaymentByIdAsync(id);
            if (pay != null)
            {
                await _payment.DeletePaymentAsync(id);
                return Ok(pay);
            }
            return NoContent();
        }



    }
}
