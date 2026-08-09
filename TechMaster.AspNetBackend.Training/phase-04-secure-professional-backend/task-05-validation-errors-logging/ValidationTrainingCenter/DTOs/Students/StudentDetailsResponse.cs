namespace TrainingCenter.Api.DTOs.Students
{
    public class StudentDetailsResponse
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int EnrollmentCount { get; set; }
    }
}
