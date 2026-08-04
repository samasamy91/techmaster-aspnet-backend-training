namespace TrainingCenterQueries.DTOs.Reports
{
    public class TopTrackResponse
    {
        public int TrainingTrackId { get; set; }

        public string TrackTitle { get; set; } = string.Empty;

        public int ActiveEnrollmentCount { get; set; }

        public int Capacity { get; set; }

        public int RemainingSeats { get; set; }
    }
}
