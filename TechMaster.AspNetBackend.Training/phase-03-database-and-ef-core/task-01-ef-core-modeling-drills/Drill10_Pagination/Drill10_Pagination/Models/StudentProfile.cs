namespace Drill02_OneToOneStudentProfile.Models
{
    public class StudentProfile
    {
        public int Id { get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }
        public string EmergencyPhone { get; set; }
        public DateTime DateOfBirth {  get; set; }
        public int StudentId { get; set; }
        public Student? Student {  get; set; }
    }
}
