using StudentManagement.DTOs;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly List<Student> students = new();
        private int Id = 4;
        private StudentResponse Map(Student student)
        {
            return new StudentResponse
            {
                StudentId = student.StudentId,
                FullName = student.FullName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                TrackName = student.TrackName,
                EnrollmentDate = student.EnrollmentDate,
                IsActive = student.IsActive,
                GitHubProfileUrl = student.GitHubProfileUrl,
                LinkedInProfileUrl = student.LinkedInProfileUrl
            };
        }
        public StudentService()
        {
            students.AddRange(new[]
            {
                new Student
                {
                    StudentId = 1,
                    FullName = "Sama Samy",
                    Email = "ahmed@example.com",
                    PhoneNumber = "01000000001",
                    TrackName = ".NET",
                    EnrollmentDate = DateTime.UtcNow.AddDays(-30),
                    IsActive = true,
                    GitHubProfileUrl = "https://github.com/ahmed",
                    LinkedInProfileUrl = "https://linkedin.com/in/ahmed"
                },
                new Student
                {
                    StudentId = 2,
                    FullName = "Sara Mohamed",
                    Email = "sara@example.com",
                    PhoneNumber = "01000000002",
                    TrackName = "Java",
                    EnrollmentDate = DateTime.UtcNow.AddDays(-20),
                    IsActive = true
                },
                new Student
                {
                    StudentId = 3,
                    FullName = "Zeina Ahmed",
                    Email = "omar@example.com",
                    PhoneNumber = "01000000003",
                    TrackName = "Flutter",
                    EnrollmentDate = DateTime.UtcNow.AddDays(-10),
                    IsActive = false
                }
            });
        }
        public StudentResponse Create(CreateStudentRequest request)
        {
            if (students.Any(s => s.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Email already exists ");
            }
            var student = new Student
            {
                StudentId = Id++,
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                TrackName= request.TrackName,
                EnrollmentDate = DateTime.UtcNow,
                IsActive = true,
                GitHubProfileUrl = request.GitHubProfileUrl,
                LinkedInProfileUrl = request.LinkedInProfileUrl
            };
            students.Add(student);
            return Map(student);
        }
        public IEnumerable<StudentResponse> GetAll(string? search,string? trackName,bool? isActive,int pageNumber,int pageSize)
        {
            var query = students.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s=>s.FullName.Contains(search,StringComparison.OrdinalIgnoreCase)||
                s.Email.Contains(search, StringComparison.OrdinalIgnoreCase));
            }
            if(!string.IsNullOrWhiteSpace(trackName))
            {
                query = query.Where(s => s.TrackName.Equals(trackName, StringComparison.OrdinalIgnoreCase));
            }
            if (isActive.HasValue)
            {
                query = query.Where(s => s.IsActive == isActive.Value);
            }
            query = query.Skip((pageNumber-1)*pageSize).Take(pageSize);
            return query.Select(Map);
        }
        public StudentResponse? GetById(int id)
        {
            var student = students.FirstOrDefault(s => s.StudentId == id);
            if(student == null)
            {
                return null;
            }
            return Map(student);
        }
        public StudentResponse Update(int id,UpdateStudentRequest request)
        {
            var student = students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
                return null;
            if(students.Any(s=>s.StudentId!=id && s.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Email already exists ");
            }
            student.FullName = request.FullName;
            student.Email = request.Email;
            student.TrackName = request.TrackName;
            student.PhoneNumber = request.PhoneNumber;
            student.GitHubProfileUrl = request.GitHubProfileUrl;
            student.LinkedInProfileUrl = request.LinkedInProfileUrl;

            return Map(student);
        }
        public bool UpdateStatus(int id,bool isActive)
        {
            var student = students.FirstOrDefault(s=>s.StudentId == id);
            if (student == null)
                return false;
            student.IsActive = isActive;
            return true;
        }
        public StudentStatsResponse StudentStats()
        {
            return new StudentStatsResponse
            {
                TotalStudents = students.Count(),
                ActiveStudents = students.Count(s => s.IsActive),
                InActiveStudents = students.Count(s => !s.IsActive),
                StudentsPerTrack = students.GroupBy(s => s.TrackName).ToDictionary(
                    g => g.Key,
                    g => g.Count())
            };
        }
        public IEnumerable<StudentResponse> GetByTrack(string trackName)
        {
            return students.Where(s => s.TrackName.Equals(trackName, StringComparison.OrdinalIgnoreCase)).Select(Map);
        }
        public bool Delete(int id)
        {
            var student = students.FirstOrDefault(s=>s.StudentId ==id);
            if(student == null)
                return false;
            students.Remove(student);
            return true;
        }
    }
}
