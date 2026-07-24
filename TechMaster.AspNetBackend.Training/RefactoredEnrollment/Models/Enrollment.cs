using Drill02_OneToOneStudentProfile.Models;
using Drill03_OneToManyInstructorTracks.Models;
using Drill05_PaymentSummary.Models;

namespace Drill04_ManyToManyEnrollment.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public Student? Student { get; set; }

        public int TrainingTrackId { get; set; }

        public TrainingTrack? TrainingTrack { get; set; }

        public string Status { get; set; } = "Active";

        public DateTime EnrollmentDate { get; set; }
        public bool IsDeleted { get; set; }

        public decimal? FinalGrade { get; set; }
        public PaymentSummary? PaymentSummary { get; set; }
    }
}
