namespace TrainingCenter.Api.DTOs.Reports
{
    public class DashboardSummaryResponse
    {
        public int StudentsCount { get; set; }
        public int TracksCount { get; set; }
        public int ActiveEnrollments { get; set; }
        public decimal Revenue { get; set; }
        public int UnpaidCount { get; set; }
    }
}
