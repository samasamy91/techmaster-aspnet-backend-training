namespace Drill03_OneToManyInstructorTracks.DTOs
{
    public class CreateTrackDto
    {
        public string Name { get; set; } 

        public int DurationInMonths { get; set; }

        public int InstructorId { get; set; }
    }
}
