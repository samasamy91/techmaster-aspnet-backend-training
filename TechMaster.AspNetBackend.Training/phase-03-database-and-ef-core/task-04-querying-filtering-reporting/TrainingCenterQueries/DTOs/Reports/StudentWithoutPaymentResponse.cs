using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenterQueries.DTOs.Reports
{
    public class StudentWithoutPaymentResponse
    {
        public int StudentId { get; set; }

        public string FullName { get; set; } 

        public string Email { get; set; } 

        public int EnrollmentId { get; set; }

        public string TrackTitle { get; set; } = string.Empty;

        public EnrollmentStatus EnrollmentStatus { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
