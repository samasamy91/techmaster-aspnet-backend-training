namespace TrainingCenterQueries.DTOs.Reports
{
    public class InstructorWorkloadResponse
    {
        public int InstructorId { get; set; }
        public string InstructorName { get; set; } 
        public int TrackCount { get; set; }
        public int ActiveStudentCount { get; set; }
    }
}
