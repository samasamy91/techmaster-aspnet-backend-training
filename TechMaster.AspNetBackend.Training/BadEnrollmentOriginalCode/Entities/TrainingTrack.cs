using System.ComponentModel.DataAnnotations;
using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenter.Api.Entities
{
    public class TrainingTrack
    {
        public int TrainingTrackId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(20)]
        public string Code { get; set; }
       
        [Range(1,1000)]
        public int Capacity { get; set; }
       
        public int InstructorId { get; set; }

        public Instructor Instructor { get; set; } = null!;
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();


    }
}
