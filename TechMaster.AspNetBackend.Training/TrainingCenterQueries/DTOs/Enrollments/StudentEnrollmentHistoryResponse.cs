using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenterQueries.DTOs.Enrollments
{
    public class StudentEnrollmentHistoryResponse
    {
        public int EnrollmentId { get; set; }

        public int TrainingTrackId { get; set; }

        public string TrackTitle { get; set; } 

        public EnrollmentStatus Status { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public decimal ProgressPercentage { get; set; }

        public string? FinalResult { get; set; }
    }
}
