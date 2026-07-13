using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiRoutingDrills.Models;
using ApiRoutingDrills.DTOs;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private static readonly List<Note> Notes = new();
        private static int nextId = 1;
        [HttpPost]
        public IActionResult Create([FromBody] CreateNoteRequest request)
        {
            var note = new Note
            {
                Id = nextId++,
                Title = request.Title,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow
            };
            Notes.Add(note);
            return Ok(note);
        }
        //Drill07
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Notes);
        }
        //Drill08
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var note = Notes.FirstOrDefault(n => n.Id == id);
            if(note == null)
            {
                return NotFound(new
                {
                    message = "Note not found"
                });
            }
            return Ok(note);
        }
        //Drill09
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateNoteRequest request)
        {
            var note = Notes.FirstOrDefault(n=>n.Id == id);
            if (note == null)
            {
                return NotFound(new
                {
                    message = "Note not found"
                });
            }
            note.Title = request.Title;
            note.Content = request.Content;
            return Ok(note);
        }
        //Drill10
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var note = Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound(new
                {
                    message = "Note not found"
                });
            }
            Notes.Remove(note);
            return NoContent();
        }
    }
}
