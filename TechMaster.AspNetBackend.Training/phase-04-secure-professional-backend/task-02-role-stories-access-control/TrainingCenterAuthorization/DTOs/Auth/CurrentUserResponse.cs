using TrainingCenterAuthTask01.Entities.Enums;

namespace TrainingCenterAuthTask01.DTOs.Auth
{
    public class CurrentUserResponse
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public int? LinkedStudentId { get; set; }
        public int? LinkkedInstructorId { get; set; }
    }
}
