using System.ComponentModel.DataAnnotations;

namespace TrainingCenterAuthTask01.DTOs.Auth
{
    public class ChangePasswordRequest
    {
        [Required]
        [MinLength(8)]
        public string CurrentPassword { get; set; }
        [Required]
        [MinLength(8)]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
