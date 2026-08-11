namespace SecurePlatformUpgrade.DTOs.TrackSession
{
    public class CreateTrackSessionRequest
    {
        public DateTime SessionDate { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? MeetingLink { get; set; }
    
    }
}
