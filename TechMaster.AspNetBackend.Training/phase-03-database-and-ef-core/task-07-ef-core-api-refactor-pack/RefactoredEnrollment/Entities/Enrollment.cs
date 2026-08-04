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
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Student Student { get; set; } = null!;
        public TrainingTrack TrainingTrack { get; set; } = null!;
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
