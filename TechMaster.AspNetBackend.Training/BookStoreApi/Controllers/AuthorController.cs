using BookStoreApi.DTOs;
using BookStoreApi.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService authorService;
        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(authorService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var author = authorService.GetById(id);
            if(author == null)
            {
                return NotFound(new
                {
                    message = "Author not found"
                });
            }
            return Ok(author);
        }
        [HttpPost]
        public IActionResult Create(CreateAuthorRequest request)
        {
            try
            {
                var author = authorService.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = author.AuthorId }, author);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = authorService.Delete(id);
            if (!deleted)
            {
                return NotFound(new{
                    message = "Author not found"
                });
            }
            return NoContent();
        }
    }
}
