using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiRoutingDrills.Services;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Drill4_ConverterController : ControllerBase
    {
        private readonly Drill4_ConverterService converterService;
        public Drill4_ConverterController(Drill4_ConverterService converterService)
        {
            this.converterService = converterService;
        }
        [HttpGet]
        public IActionResult CelToFah([FromQuery] decimal value)
        {
            decimal fah = converterService.ConvertCelsiusToFahrenheit(value);
            return Ok(new
            {
                cel = Math.Round(value, 2),
                fah = Math.Round(fah, 2),
                formulaUsed = "F=(C*9/5)+32"
            });
        }
    }
}
