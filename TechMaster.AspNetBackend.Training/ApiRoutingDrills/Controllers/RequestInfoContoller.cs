using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/request-info")]
    [ApiController]
    public class RequestInfoContoller : ControllerBase
    {
        //Drill13
        [HttpGet]
        public IActionResult GetRequestInfo()
        {
            var studentName = Request.Headers["X-Student-Name"].FirstOrDefault();
            if(string.IsNullOrWhiteSpace(studentName))
            {
                return BadRequest(new
                {
                    error = "X-Student-Name header is required"
                });
            }
            return Ok(new
            {
                studentName,
                requestPath = Request.Path.Value
            });
        }
    }
}
