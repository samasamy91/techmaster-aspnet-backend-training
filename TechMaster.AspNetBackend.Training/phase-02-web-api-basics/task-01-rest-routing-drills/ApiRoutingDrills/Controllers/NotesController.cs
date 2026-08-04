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
        //Drill11
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new
                {
                    error = "Keyword cannot be empty"
                });
            }
            var matching = Notes.Where(n => n.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            (n.Content != null && n.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                .ToList();
            return Ok(matching);
        }
        //Drill12
        [HttpGet("pages")]
        public IActionResult GetAllPages([FromQuery] int pageNum = 1, [FromQuery] int pageSize = 10)
        {
            if(pageNum <= 0)
            {
                return BadRequest(new
                {
                    error = "Page number must be greater than 0"
                });
            }
            if (pageSize <1 || pageSize >50)
            {
                return BadRequest(new
                {
                    error = "Page size must be between 1 and 50"
                });
            }
            int totalCount = Notes.Count;
            var items = Notes.Skip((pageNum -1)* pageSize).Take(pageSize).ToList();
            return Ok(new
            {
                pageNum,pageSize,totalCount,items
            });
        }
    }
}
