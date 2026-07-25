namespace TrainingCenter.Api.DTOs.Reports
{
    public class DashboardSummaryResponse
    {
        public int TotalStudents { get; set; }
        public int TotalInstructor {  get; set; }
        public int TotalTrack {  get; set; }
        public int ActiveEnrollments { get; set; }
        public decimal TotalRevenue { get; set; }

    }
}
