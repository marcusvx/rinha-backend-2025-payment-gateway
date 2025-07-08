using Microsoft.AspNetCore.Mvc;
using PaymentMediator.Models;
using PaymentMediator.Services;

namespace PaymentMediator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
        {
            await _paymentService.ProcessAsync(request);
            return Accepted(); // HTTP 202 (assíncrono)
        }

        [HttpGet("/payments-summary")]
        public async Task<IActionResult> GetSummary([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var summary = await _paymentService.GetSummary(from, to);
            return Ok(summary);
        }
    }
}