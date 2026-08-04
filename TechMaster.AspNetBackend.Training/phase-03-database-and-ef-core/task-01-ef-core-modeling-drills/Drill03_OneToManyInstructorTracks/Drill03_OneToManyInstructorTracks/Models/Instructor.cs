namespace Drill03_OneToManyInstructorTracks.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //one to many tracks
        public ICollection<TrainingTrack> TrainingTracks { get; set; } = new List<TrainingTrack>();
    }
}
