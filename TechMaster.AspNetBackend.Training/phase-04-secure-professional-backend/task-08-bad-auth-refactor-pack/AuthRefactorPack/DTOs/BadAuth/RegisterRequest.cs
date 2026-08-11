using System.ComponentModel.DataAnnotations;
using TrainingCenterAuthTask01.Entities.Enums;

namespace AuthRefactorPack.DTOs.BadAuth
{
    public class RegisterRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public UserRole Role { get; set; }
        public string? Specialization { get; set; }
    }
}
