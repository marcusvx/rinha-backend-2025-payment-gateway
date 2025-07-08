using PaymentMediator.Models;

namespace PaymentMediator.Services
{
    public interface IPaymentService
    {
        Task<PaymentSummary> GetSummary(DateTime? from, DateTime? to);

        Task ProcessAsync(PaymentRequest request);
    }
}