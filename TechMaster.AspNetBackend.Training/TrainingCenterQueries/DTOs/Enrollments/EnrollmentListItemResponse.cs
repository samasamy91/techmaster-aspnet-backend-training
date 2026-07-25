using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenterQueries.DTOs.Enrollments
{
    public class EnrollmentListItemResponse
    {
        public int EnrollmentId { get; set; }

        public string StudentName { get; set; } 

        public string TrackTitle { get; set; } 

        public EnrollmentStatus Status { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public decimal ProgressPercentage { get; set; }
    }
}
