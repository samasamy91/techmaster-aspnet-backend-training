using Drill02_OneToOneStudentProfile.Data;
using Drill04_ManyToManyEnrollment.DTOs;
using Drill04_ManyToManyEnrollment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Drill04_ManyToManyEnrollment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly AppDbContext context;
        public EnrollmentsController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpPost]
        public IActionResult Enroll(CreateEnrollmentDto dto)
        {
            bool exists = context.Enrollments.Any(e => e.StudentId == dto.StudentId &&
            e.TrainingTrackId == dto.TrainingTrackId && e.Status == "Active");

            if (exists)
                return BadRequest("Student already has active enrollment");
            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                TrainingTrackId = dto.TrainingTrackId,
                Status = dto.Status,
                EnrollmentDate = DateTime.UtcNow
            };
            context.Enrollments.Add(enrollment);
            context.SaveChanges();
            return Ok(enrollment);
        }
        [HttpGet("{id}")]
        public IActionResult GetEnrollment(int id)
        {
            var enrollment = context.Enrollments.Include(e => e.PaymentSummary).FirstOrDefault(e => e.Id == id);
            if (enrollment == null)
                return NotFound();
            return Ok(new
            {
                enrollment.Id,
                enrollment.Status,
                Payment = enrollment.PaymentSummary
            });
        }
    }
}
