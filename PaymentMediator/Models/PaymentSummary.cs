namespace PaymentMediator.Models
{
    public class PaymentSummary
    {
        public SummaryDetails Default { get; set; } = new SummaryDetails();
        public SummaryDetails Fallback { get; set; } = new SummaryDetails();
    }

    public class SummaryDetails
    {
        public long TotalRequests { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
