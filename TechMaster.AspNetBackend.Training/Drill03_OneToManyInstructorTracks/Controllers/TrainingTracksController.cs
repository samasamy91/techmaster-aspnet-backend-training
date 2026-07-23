using Drill02_OneToOneStudentProfile.Data;
using Drill03_OneToManyInstructorTracks.DTOs;
using Drill03_OneToManyInstructorTracks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
