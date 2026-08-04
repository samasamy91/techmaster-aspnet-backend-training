namespace TrainingCenterQueries.DTOs.Tracks
{
    public class TrackAvailableSeatsResponse
    {
        public int TrainingTrackId { get; set; }
        public string Title { get; set; }
        public int Capacity { get; set; }
        public int ActiveEnrollment {  get; set; }
        public int RemainingSeats {  get; set; }
    }
}
