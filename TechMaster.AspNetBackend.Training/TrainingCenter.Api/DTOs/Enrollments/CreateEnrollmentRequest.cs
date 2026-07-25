using System.ComponentModel.DataAnnotations;

namespace TrainingCenter.Api.DTOs.Enrollments
{
    public class CreateEnrollmentRequest
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int TrainingTrackId {  get; set; }
    }
}
