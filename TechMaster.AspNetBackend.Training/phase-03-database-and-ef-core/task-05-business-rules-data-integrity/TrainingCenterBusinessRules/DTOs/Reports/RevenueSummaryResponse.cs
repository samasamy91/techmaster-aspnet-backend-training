namespace TrainingCenter.Api.DTOs.Reports
{
    public class RevenueSummaryResponse
    {
        public decimal TotalRevenue { get; set; }

        public int TotalPayments { get; set; }

        public decimal AveragePayment { get; set; }
    }
}
