using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Drill3_CalculatorController : ControllerBase
    {
        [HttpGet("Add")]
        public IActionResult Add([FromQuery] decimal a, [FromQuery] decimal b)
        {
            return Ok(new
            {
                a,
                b,
                operation = "Add",
                result = a + b
            });
        }
    }
}
