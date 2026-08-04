using System.ComponentModel.DataAnnotations;

namespace TrainingCenter.Api.Entities
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string? Specialization { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(1000)]
        public string ? Bio {  get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<TrainingTrack> TrainingTracks { get; set; } = new List<TrainingTrack>();
    }
}
