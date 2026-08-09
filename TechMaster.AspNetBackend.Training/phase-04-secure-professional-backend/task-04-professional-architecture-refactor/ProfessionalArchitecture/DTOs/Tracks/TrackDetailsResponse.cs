using System.ComponentModel.DataAnnotations;
using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenter.Api.DTOs.Tracks
{
    public class TrackDetailsResponse
    {
        public int TrainingTrackId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public TrackLevel Level { get; set; }
        public TrackStatus Status { get; set; }
        public int Capacity { get; set; }
        public int EnrolledStudents { get; set; }
        public int AvailableSeats => Capacity - EnrolledStudents;
        public string InstructorName { get; set; }
    }
}
