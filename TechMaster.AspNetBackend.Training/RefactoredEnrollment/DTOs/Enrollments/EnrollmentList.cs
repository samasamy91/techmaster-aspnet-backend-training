using TrainingCenter.Api.Entities.Enums;

namespace RefactoredEnrollment.DTOs.Enrollments
{
    public class EnrollmentList
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string TrackName { get; set; }
        public EnrollmentStatus Status { get; set; }
    }
}
