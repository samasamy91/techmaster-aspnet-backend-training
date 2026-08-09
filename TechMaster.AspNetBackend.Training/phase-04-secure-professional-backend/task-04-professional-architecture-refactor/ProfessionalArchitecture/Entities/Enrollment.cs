using System.ComponentModel.DataAnnotations;
using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenter.Api.Entities
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int TrainingTrackId { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        [Required]
        public EnrollmentStatus Status { get; set; }
        [Range(0,100)]
        public decimal ProgressPercentage { get; set; }
        [MaxLength(100)]
        public string? FinalResult { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public Student Student { get; set; } = null!;
        public TrainingTrack TrainingTrack { get; set; } = null!;
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
