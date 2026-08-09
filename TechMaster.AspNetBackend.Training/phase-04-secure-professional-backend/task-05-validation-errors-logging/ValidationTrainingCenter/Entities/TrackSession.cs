using TrainingCenter.Api.Entities;

namespace SecurePlatformUpgrade.Entities
{
    public class TrackSession
    {
        public int TrackSessionId {  get; set; }
        public int TrainingTrackId { get; set; }
        public DateTime SessionDate { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? MeetionLink {  get; set; }
        public bool IsCompleted { get; set; }
        public int CreatedByInstructorId { get; set; }
        public TrainingTrack TrainingTrack { get; set; }
        public Instructor CreatedByInstructor { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set;}
    }
}
