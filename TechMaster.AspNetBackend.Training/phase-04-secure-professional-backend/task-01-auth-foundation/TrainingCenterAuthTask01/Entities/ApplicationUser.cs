using Microsoft.AspNetCore.Identity;

namespace TrainingCenterAuthTask01.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName {  get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public int? StudentId { get; set; }
        public int? InstructorId { get; set;}

    }
}
