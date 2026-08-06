using System.ComponentModel.DataAnnotations;

namespace TrainingCenterAuthTask01.DTOs.Auth
{
    public class AuthResponse
    {
        public string AccessToken {  get; set; }
        public DateTime ExpiresAt { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
