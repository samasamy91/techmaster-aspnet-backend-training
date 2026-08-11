namespace SecurePlatformUpgrade.DTOs.Instructors
{
    public class TrackSessionResponse
    {
        public int TrackSessionId { get; set; }
        public int TrainingTrackId { get; set; }
        public DateTime SessionDate { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? MeetionLink { get; set; }
        public bool IsCompleted { get; set; }
        public int CreatedByInstructorId { get; set; }
    }
}
