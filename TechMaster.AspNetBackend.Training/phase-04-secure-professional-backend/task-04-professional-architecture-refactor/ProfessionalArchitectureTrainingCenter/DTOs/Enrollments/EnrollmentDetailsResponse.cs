using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenter.Api.DTOs.Enrollments
{
    public class EnrollmentDetailsResponse
    {
        public int EnrollmentId { get; set; }
        public string StudentName { get; set; }
        public string TrackTitle { get; set; }
        public EnrollmentStatus Status {  get; set; }
        public decimal ProgressPercentage { get; set; }
        public string? FinalResult { get; set; }
    }
}
