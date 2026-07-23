using Drill02_OneToOneStudentProfile.Data;
using Drill02_OneToOneStudentProfile.DTOs;
using Drill02_OneToOneStudentProfile.Models;
using Drill08_AuditFields.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Drill02_OneToOneStudentProfile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext context;
        public StudentsController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var students = context.Students
            .IgnoreQueryFilters().ToList();
            return Ok(students);
        }
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.TrainingTrack)
                .Where(s => s.Id == id)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    Tracks = s.Enrollments.Select(e => new
                    {
                        e.TrainingTrack!.Name,
                        e.Status,
                        e.EnrollmentDate,
                        e.FinalGrade
                    })
                })
                .FirstOrDefault();

            if (student == null)
                return NotFound();

            return Ok(student);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = context.Students.Find(id);

            if (student == null)
                return NotFound();

            student.IsDeleted = true;
            student.DeletedAt = DateTime.UtcNow;

            context.SaveChanges();

            return NoContent();
        }
        [HttpPost]
        public IActionResult Create(StudentDto dto)
        {
            var student = new Student
            {
                Name = dto.FullName,
                Email = dto.Email
            };
            context.Students.Add(student);
            context.SaveChanges();
            return Ok(student);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,UpdateStudentDto dto)
        {
            var student = context.Students.Find(id);
            if(student == null)
                return NotFound();
            student.Name = dto.Name;
            student.Email = dto.Email;
            context.SaveChanges();
            return Ok(student);
        }
    }
}
