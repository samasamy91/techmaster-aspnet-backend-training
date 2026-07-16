using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefactoredApi.DTOs;
using RefactoredApi.Services.IServices;

namespace RefactoredApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(productService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = productService.GetById(id);
            if(product == null)
            {
                return NotFound(new
                {
                    message = "Product not found"
                });
            }
            return Ok(product);
        }
        [HttpPost]
        public IActionResult Create(CreateProductRequest request)
        {
            try
            {
                var product = productService.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
            catch(Exception ex) 
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }
    }
}
