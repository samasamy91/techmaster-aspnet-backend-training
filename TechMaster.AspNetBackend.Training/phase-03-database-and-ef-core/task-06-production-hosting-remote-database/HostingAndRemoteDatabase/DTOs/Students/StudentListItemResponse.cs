using System.Reflection.Metadata;

namespace TrainingCenter.Api.DTOs.Students
{
    public class StudentListItemResponse
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int EnrollmentCount { get; set; }
    }
}
