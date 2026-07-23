using Drill02_OneToOneStudentProfile.Data;
using Drill03_OneToManyInstructorTracks.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Drill03_OneToManyInstructorTracks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly AppDbContext context;
        public InstructorsController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet("{id}/tracks")]
        public IActionResult GetTracks(int id)
        {
            var instructor = context.Instructors.Include(i => i.TrainingTracks)
                .Where(i => i.Id == id).Select(i => new InstructorTracksDto
                {
                    Id = i.Id,
                    FullName = i.Name,
                    Tracks = i.TrainingTracks.Select(t => t.Name).ToList()
                }).FirstOrDefault();
            if (instructor == null)
                return NotFound();
            return Ok(instructor);
        }
    }
}
