namespace StudentManagement.DTOs
{
    public class StudentResponse
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TrackName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsActive { get; set; }
        public string? GitHubProfileUrl { get; set; }
        public string? LinkedInProfileUrl { get; set; }
    }
}
