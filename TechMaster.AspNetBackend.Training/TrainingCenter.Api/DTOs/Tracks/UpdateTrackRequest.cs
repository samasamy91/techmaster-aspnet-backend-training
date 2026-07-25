using System.ComponentModel.DataAnnotations;
using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenter.Api.DTOs.Tracks
{
    public class UpdateTrackRequest
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public TrackLevel Level { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int InstructorId { get; set; }
    }
}
