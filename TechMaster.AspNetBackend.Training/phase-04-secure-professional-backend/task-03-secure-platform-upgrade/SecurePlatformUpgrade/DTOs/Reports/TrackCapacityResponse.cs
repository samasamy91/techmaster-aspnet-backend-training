namespace TrainingCenter.Api.DTOs.Reports
{
    public class TrackCapacityResponse
    {
        public int TrainingTrackId { get; set; }

        public string TrackTitle { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public int EnrolledStudents { get; set; }

        public int AvailableSeats { get; set; }
    }
}
