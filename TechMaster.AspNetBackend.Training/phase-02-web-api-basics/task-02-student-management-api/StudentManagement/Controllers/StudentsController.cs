using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs;
using StudentManagement.Services;


namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;
        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }
        [HttpPost]
        public IActionResult Create(CreateStudentRequest request)
        {
            try
            {
                var student = studentService.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = student.StudentId }, student);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }
        [HttpGet]
        public IActionResult GetAll(string? search,string? trackName,bool? isActive,int pageNumber=1,int pageSize = 10)
        {
            if (pageNumber <= 0)
            {
                return BadRequest(new
                {
                    error = "Page Number must be greater than 0"
                });
            }
            if(pageSize<1 || pageSize > 50)
            {
                return BadRequest(new
                {
                    error = "Page size must be between 1-50"
                });
            }
            var students = studentService.GetAll(search, trackName, isActive, pageNumber, pageSize);
            return Ok(students);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = studentService.GetById(id);
            if(student == null)
            {
                return NotFound(new
                {
                    message = "Student not found"
                });
            }
            return Ok(student);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateStudentRequest request)
        {
            try
            {
                var student = studentService.Update(id, request);
                if(student == null)
                {
                    return NotFound(new
                    {
                        message = "Student Not Found"
                    });
                }
                return Ok(student);
            }catch(InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }
        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id , UpdateStudentStatusRequest request)
        {
                var student = studentService.UpdateStatus(id, request.IsActive);
                if (!student)
                {
                    return NotFound(new
                    {
                        message = "Student Not Found"
                    });
                }
            return Ok(new
            {
                message = "Student status updated successfully",
                isActive = request.IsActive
            }); 
        }
        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            return Ok(studentService.StudentStats());
        }
        [HttpGet("by-track/{trackName}")]
        public IActionResult GetByTrackName(string trackName)
        {
            var student = studentService.GetByTrack(trackName);
                if (student == null)
                {
                    return NotFound(new
                    {
                        message = "Student Not Found"
                    });
                }
            return Ok(student);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = studentService.Delete(id);
                if (!student)
                {
                    return NotFound(new
                    {
                        message = "Student Not Found"
                    });
                }
            return NoContent();
        }
    }
}
