namespace Drill09_ProjectionDTO.DTOs
{
    public class TrackDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationInMonths { get; set; }
        public string InstructorName { get; set; } = string.Empty;
        public List<string> Students { get; set; } = new();
    }
}
