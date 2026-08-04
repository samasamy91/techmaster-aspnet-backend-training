namespace TrainingCenter.Api.DTOs.Reports
{
    public class RevenueSummaryResponse
    {
        public decimal TotalRevenue { get; set; }

        public int TotalPayments { get; set; }

        public decimal AveragePayment { get; set; }
        public int PaidCount { get; set; }

        public int PendingCount { get; set; }

        public int FailedCount { get; set; }
    }
}
