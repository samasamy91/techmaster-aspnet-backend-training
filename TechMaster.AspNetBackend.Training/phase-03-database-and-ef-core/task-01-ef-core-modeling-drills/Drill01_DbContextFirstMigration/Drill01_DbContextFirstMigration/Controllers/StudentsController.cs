using Drill01_DbContextFirstMigration.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Drill01_DbContextFirstMigration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext context;
        public StudentsController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(context.Students.ToList());
        }
    }
}
