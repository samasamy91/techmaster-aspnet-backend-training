using BookStoreApi.DTOs;
using BookStoreApi.Services;
using BookStoreApi.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;
        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery]string? search, [FromQuery] int? authorId, [FromQuery] int? categoryId, [FromQuery] bool? available,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var books = bookService.GetAll(search, categoryId, authorId, available, pageNumber, pageSize);
            return Ok(books);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = bookService.GetById(id);
            if(book == null)
            {
                return NotFound(new{
                    message = "Book not exists"
                });
            }
            return Ok(book);
        }
        [HttpPost]
        public IActionResult Create(CreateBookRequest request)
        {
            try
            {
                var book = bookService.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = book.BookId }, book);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateBookRequest request)
        {
            try
            {
                var updated = bookService.Update(id, request);
                if (!updated)
                {
                    return NotFound(new
                    {
                        Message = "Book not found."
                    });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = bookService.Delete(id);
            if (!deleted)
            {
                return NotFound(new
                {
                    Message = "Book not found."
                });
            }
            return NoContent();
        }
        [HttpGet("reports/summary")]
        public IActionResult GetSummary()
        {
            return Ok(bookService.GetSummary());
        }
    }
}
