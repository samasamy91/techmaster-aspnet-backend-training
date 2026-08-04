using Drill02_OneToOneStudentProfile.Data;
using Drill03_OneToManyInstructorTracks.DTOs;
using Drill03_OneToManyInstructorTracks.Models;
using Drill09_ProjectionDTO.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Drill03_OneToManyInstructorTracks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingTracksController : ControllerBase
    {
        private readonly AppDbContext context;

        public TrainingTracksController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpPost]
        public IActionResult Create(CreateTrackDto dto)
        {
            var instructorExists = context.Instructors.Any(i => i.Id == dto.InstructorId);
            if (!instructorExists)
                return BadRequest("Instructor does not exist");

            var track = new TrainingTrack
            {
                Name = dto.Name,
                DurationInMonths = dto.DurationInMonths,
                InstructorId = dto.InstructorId,
            };
            context.TrainingTracks.Add(track);
            context.SaveChanges();
            return CreatedAtAction(nameof(Create), new { id = track.Id }, track);
        }
        [HttpGet("{id}")]
        public IActionResult GetTrack(int id)
        {
            var track = context.TrainingTracks
                .Where(t=>t.Id ==  id).Select(t=>new TrackDetailsDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    DurationInMonths = t.DurationInMonths,
                    InstructorName = t.Instructor.Name,
                    Students = t.Enrollments.Select(e=>e.Student.Name).ToList()
                })
                .FirstOrDefault();

            if (track == null)
                return NotFound();

            return Ok(track);
        }
    }
}
