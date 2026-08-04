using System.ComponentModel.DataAnnotations;

namespace RefactoredEnrollment.DTOs.Enrollments
{
    public class CreateEnrollmentRequest
    {
        [Required]
        public int StudentId {  get; set; }
        [Required]
        public int TrainingTrackId { get; set; }
    }
}
