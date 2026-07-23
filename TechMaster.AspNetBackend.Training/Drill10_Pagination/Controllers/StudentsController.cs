using Drill02_OneToOneStudentProfile.Data;
using Drill02_OneToOneStudentProfile.DTOs;
using Drill02_OneToOneStudentProfile.Models;
using Drill08_AuditFields.DTOs;
using Drill09_ProjectionDTO.DTOs;
using Drill10_Pagination.DTOs;
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
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var students = context.Students.Select(s=>new StudentListItemDto
        //    {
        //        Id = s.Id,
        //        Name = s.Name,
        //        Email = s.Email,
        //    })
        //    .IgnoreQueryFilters().ToList();
        //    return Ok(students);
        //}
        [HttpGet]
        public async Task<IActionResult> GetStudents(int pageNum=1,int pageSize = 10)
        {
            if (pageNum <= 0)
                return BadRequest("Page number must be greater than zero.");

            if (pageSize < 1 || pageSize > 50)
                return BadRequest("Page size must be between 1 and 50.");

            int totalCount = await context.Students.CountAsync();

            int skip = (pageNum - 1) * pageSize;

            var students = await context.Students
                .OrderBy(s => s.Id)
                .Skip(skip)
                .Take(pageSize)
                .Select(s => new StudentListItemDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email
                })
                .ToListAsync();

            var result = new PaginationResult<StudentListItemDto>
            {
                Items = students,
                TotalCount = totalCount,
                PageNumber = pageNum,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };

            return Ok(result);
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
