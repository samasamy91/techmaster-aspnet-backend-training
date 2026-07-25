using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenterQueries.DTOs.Enrollments
{
    public class TrackStudentResponse
    {
        public int StudentId { get; set; }

        public string FullName { get; set; } 

        public string Email { get; set; } 

        public EnrollmentStatus Status { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
