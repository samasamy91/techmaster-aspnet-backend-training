namespace Drill04_ManyToManyEnrollment.DTOs
{
    public class CreateEnrollmentDto
    {
        public int StudentId { get; set; }

        public int TrainingTrackId { get; set; }

        public string Status { get; set; } = "Active";
    }
}
