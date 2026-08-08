using System.ComponentModel.DataAnnotations;
using TrainingCenterAuthTask01.Entities.Enums;

namespace TrainingCenterAuthTask01.DTOs.Auth
{
    public class AuthResponse
    {
        public string AccessToken {  get; set; }
        public DateTime ExpiresAt { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}
