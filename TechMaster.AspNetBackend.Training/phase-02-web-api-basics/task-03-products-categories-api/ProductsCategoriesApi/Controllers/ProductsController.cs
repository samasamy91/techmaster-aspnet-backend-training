using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Services.IServices;

namespace ProductsCategoriesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        public ActionResult GetAll(string? search, int? categoryId, decimal? minPrice, decimal? maxPrice, bool? isAvailable)
        {
            var products = productService.GetAll(search, categoryId, minPrice, maxPrice, isAvailable);
            return Ok(products);
        } 
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
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
                return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, product);
            }catch(InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,UpdateProductRequest request)
        {
            try
            {
                var product = productService.Update(id, request);
                if( product == null)
                {
                    return NotFound(new
                    {
                        message = "Product not found"
                    });
                }
                return Ok(product);
            }catch(InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }
        [HttpPatch("{id}/stock")]
        public IActionResult UpdateStock(int id,UpdateStockRequest request)
        {
            try
            {
                var product = productService.UpdateStock(id, request.StockQuantity);
                if (!product)
                {
                    return NotFound(new
                    {
                        message = "Product not found"
                    });
                }
                return Ok(new
                {
                    message = "Stock Updated Successfully"
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = productService.Delete(id);
                if(!product)
                {
                    return NotFound(new
                    {
                        message = "Product not found"
                    });
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }
        [HttpGet("low-stock")]
        public IActionResult GetLowStock()
        {
            return Ok(productService.GetLowStock());
        }
        [HttpGet("reports/stock-value")]
        public IActionResult StockReport()
        {
            return Ok(productService.GetStockReport());
        }
    }
}
