namespace Drill03_OneToManyInstructorTracks.DTOs
{
    public class InstructorTracksDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } 

        public List<string> Tracks { get; set; } = new();
    }
}
