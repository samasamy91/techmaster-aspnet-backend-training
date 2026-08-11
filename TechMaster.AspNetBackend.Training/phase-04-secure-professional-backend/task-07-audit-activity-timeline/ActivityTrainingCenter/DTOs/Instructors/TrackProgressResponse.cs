namespace SecurePlatformUpgrade.DTOs.Instructors
{
    public class TrackProgressResponse
    {
        public int TrainingTrackId { get; set; }
        public string TrackTitle { get; set; }
        public int TotalStudents { get; set; }
        public decimal AvgProgress { get; set; }
        public int CompletedStudents { get; set; }
        public int ActiveStudents { get; set; }
    }
}
