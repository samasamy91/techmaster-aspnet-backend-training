namespace Drill03_OneToManyInstructorTracks.Models
{
    public class TrainingTrack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationInMonths { get; set; }
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
    }
}
