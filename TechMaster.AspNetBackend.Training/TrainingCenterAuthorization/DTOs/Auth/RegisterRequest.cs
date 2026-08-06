using System.ComponentModel.DataAnnotations;
using TrainingCenterAuthTask01.Entities.Enums;

namespace TrainingCenterAuthTask01.DTOs.Auth
{
    public class RegisterRequest
    {
        [Required]
        [MaxLength(100)]
        public string FullName {  get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Role {  get; set; }
    }
}
