namespace StudentManagement.DTOs
{
    public class StudentStatsResponse
    {
        public int TotalStudents {  get; set; }
        public int ActiveStudents { get; set; }
        public int InActiveStudents { get; set; }
        public Dictionary<string, int> StudentsPerTrack { get; set; } = new();

    }
}
