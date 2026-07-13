using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusCodeController : ControllerBase
    {
        //Drill14
        [HttpGet("Ok")]
        public IActionResult GetOk()
        {
            return Ok(new
            {
                message = "Request Completed Successfully"
            });
        }
        [HttpPost("create")]
        public IActionResult Create()
        {
            var resource = new
            {
                id = 1,
                name = "Sample Resource"
            };
            return CreatedAtAction(nameof(GetOk), new { id = resource.id }, resource);
        }
        [HttpDelete("delete")]
        public IActionResult Delete()
        {
            return NoContent();
        }
        [HttpGet("bad-request")]
        public IActionResult BadExample()
        {
            return BadRequest(new
            {
                error = "The request is invalid"
            });
        }
        [HttpGet("not-found")]
        public IActionResult NotFoundExample()
        {
            return NotFound(new
            {
                message = "Resource Not Found"
            });
        }
    }
}
