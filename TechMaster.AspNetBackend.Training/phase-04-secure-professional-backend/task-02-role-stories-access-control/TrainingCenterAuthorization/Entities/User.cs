using TrainingCenterAuthTask01.Entities.Enums;

namespace TrainingCenterAuthTask01.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName {  get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public UserRole Role { get; set; } 
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public int? StudentId { get; set; }
        public int? InstructorId { get; set; }
    }
}
