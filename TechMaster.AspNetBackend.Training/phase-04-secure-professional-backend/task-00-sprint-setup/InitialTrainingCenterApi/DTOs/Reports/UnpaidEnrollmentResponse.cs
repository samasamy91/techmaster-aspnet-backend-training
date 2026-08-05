namespace TrainingCenter.Api.DTOs.Reports
{
    public class UnpaidEnrollmentResponse
    {
        public int EnrollmentId { get; set; }
        public string StudentName { get; set; }
        public string TrackTitle { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal RemainingAmount { get; set; }
    }
}
