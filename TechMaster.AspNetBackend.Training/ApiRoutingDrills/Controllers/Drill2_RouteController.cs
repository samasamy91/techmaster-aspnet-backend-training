using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Drill2_RouteController : ControllerBase
    {
        [HttpGet("echo/{name}")]
        public IActionResult Echo(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new
                {
                    message = "Name cannot be empty"
                });
            }
            return Ok(new {
                originalName = name,
                message = $"Hello, {name} welcome to tachmaster api"
            });
        }
    }
}
