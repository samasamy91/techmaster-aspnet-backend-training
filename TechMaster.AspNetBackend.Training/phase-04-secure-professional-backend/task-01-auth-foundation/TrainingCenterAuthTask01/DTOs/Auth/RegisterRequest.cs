using System.ComponentModel.DataAnnotations;

namespace TrainingCenterAuthTask01.DTOs.Auth
{
    public class RegisterRequest
    {
        [Required]
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
