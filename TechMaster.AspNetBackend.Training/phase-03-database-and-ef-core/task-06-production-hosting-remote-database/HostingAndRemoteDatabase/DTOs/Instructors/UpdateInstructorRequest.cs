using System.ComponentModel.DataAnnotations;

namespace TrainingCenter.Api.DTOs.Instructors
{
    public class UpdateInstructorRequest
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Specialization { get; set; }
        public string? Bio { get; set; }
        public bool IsActive { get; set; }
    }
}
