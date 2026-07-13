using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Drill5_GradeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Calculate([FromQuery] int score)
        {
            if (score < 0 || score > 100)
            {
                return BadRequest(new
                {
                    error = "Score must be between 0 and 100"
                });
            }
            string grade;
            bool pass;
            if (score >= 90)
            {
                grade = "A";
                pass = true;
            }
            else if (score >= 80)
            {
                grade = "B";
                pass = true;
            }
            else if (score >= 70)
            {
                grade = "C";
                pass = true;
            }
            else if (score >= 50)
            {
                grade = "D";
                pass = true;
            }
            else
            {
                grade = "F";
                pass = false;
            }
            return Ok(new
            {
                score = score,
                grade = grade,
                pass = pass
            });
        }
    }
}
