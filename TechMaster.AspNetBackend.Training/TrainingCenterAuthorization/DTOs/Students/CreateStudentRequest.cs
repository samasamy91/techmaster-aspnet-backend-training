using System.ComponentModel.DataAnnotations;

namespace TrainingCenter.Api.DTOs.Students
{
    public class CreateStudentRequest
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; }
        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
    }
}
