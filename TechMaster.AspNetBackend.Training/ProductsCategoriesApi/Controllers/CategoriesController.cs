using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Services;
using ProductsCategoriesApi.Services.IServices;

namespace ProductsCategoriesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoriesController(ICategoryService categoryService)
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
            }catch(InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    error = ex.Message,
                });
            }
        }
    }
}
