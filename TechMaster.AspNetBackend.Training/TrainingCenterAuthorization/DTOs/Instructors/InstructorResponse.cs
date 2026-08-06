namespace TrainingCenter.Api.DTOs.Instructors
{
    public class InstructorResponse
    {
        public int InstructorId { get; set; }
        public string FullName {  get; set; }
        public string Email { get; set; }
        public string? Specialization { get; set; }
        public bool IsActive { get; set; }
    }
}
