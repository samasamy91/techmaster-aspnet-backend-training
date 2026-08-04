namespace TrainingCenter.Api.DTOs.Reports
{
    public class RevenueByTrackResponse
    {
        public int TrainingTrackId { get; set; }
        public string TrackTitle { get; set; }
        public decimal Revenue { get; set; }
        public int PaymentCount { get; set; }
        public int EnrollmentCount { get; set; }
    }
}
