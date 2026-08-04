using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        //Drill15
        [HttpGet("demo")]
        public IActionResult Demo([FromQuery] string? type)
        {
            if (string.Equals(type, "badrequest", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid request",
                    code = "Bad_Request",
                    details = new[]
                    {
                        "Name id Required"
                    }
                });
            }
            if (string.Equals(type, "notfound", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(new
                {
                    success = false,
                    message = "Resource not found",
                    code = "Not_Found",
                    details = new[]
                    {
                        "No resource exists with the specified ID"
                    }
                });
            }
            return Ok(new
            {
                success = true,
                message = "Use ?type=badrequest or ? type=notfound to test error responses"
            });
        }
    }
}
