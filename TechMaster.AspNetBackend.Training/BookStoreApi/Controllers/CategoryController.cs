using BookStoreApi.DTOs;
using BookStoreApi.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(categoryService.GetAll());
        }
        [HttpPost]
        public IActionResult Create(CreateCategoryRequest request)
        {
            try
            {
                var category = categoryService.Create(request);
                return CreatedAtAction(nameof(GetAll), new { id = category.CategoryId }, category);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    error = ex.Message,
                });
            }
        }
    }
}
