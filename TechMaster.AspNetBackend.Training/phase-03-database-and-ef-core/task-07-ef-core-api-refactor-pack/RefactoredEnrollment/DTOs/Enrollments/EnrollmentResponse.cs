using TrainingCenter.Api.Entities.Enums;

namespace RefactoredEnrollment.DTOs.Enrollments
{
    public class EnrollmentResponse
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string TrackName { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        public EnrollmentStatus Status { get; set; }
    }
}
