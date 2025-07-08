using PaymentMediator.Models;

namespace PaymentMediator.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _defaultProcessorUrl;
        private readonly string _fallbackProcessorUrl;

        public PaymentService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _defaultProcessorUrl = configuration["PaymentProcessorUrls:Default"];
            _fallbackProcessorUrl = configuration["PaymentProcessorUrls:Fallback"];
        }

        public async Task ProcessAsync(PaymentRequest request)
        {
            var payload = new { request.CorrelationId, request.Amount, RequestedAt = DateTime.UtcNow };

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_defaultProcessorUrl, payload);
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                // Fallback se o default falhar
                await _httpClient.PostAsJsonAsync(_fallbackProcessorUrl, payload);
            }
        }

        public Task<PaymentSummary> GetSummary(DateTime? from, DateTime? to)
        {
            // TODO: Implementar a lógica de armazenamento para obter o resumo real
            var summary = new PaymentSummary
            {
                Default = new SummaryDetails { TotalAmount = 0, TotalRequests = 0 },
                Fallback = new SummaryDetails { TotalAmount = 0, TotalRequests = 0 }
            };
            return Task.FromResult(summary);
        }
    }
}
