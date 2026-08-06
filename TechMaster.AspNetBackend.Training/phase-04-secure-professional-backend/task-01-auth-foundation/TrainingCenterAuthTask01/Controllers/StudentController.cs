using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.DTOs.Students;
using TrainingCenter.Api.Services.IServices;

namespace TrainingCenter.Api.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;
        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStudent([FromQuery] string? search,
            [FromQuery]bool? isActive, [FromQuery] PagedRequest request)
        {
            var students = await studentService.GetAllStudent(search, isActive, request.PageNumber, request.PageSize);
            return Ok(ApiResponse<object>.SuccessResponse(students, "Students retrieved successfully"));
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await studentService.GetStudentById(id);
            if (student == null)
                return NotFound(ApiResponse<string>.FailureResponse("Student not found."));
            return Ok(ApiResponse<object>.SuccessResponse(student,"Student retrieved successfully."));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentRequest request)
        {
            var student = await studentService.CreateStudent(request);
            return CreatedAtAction(
                nameof(GetById),
                new { id = student.StudentId },
                ApiResponse<object>.SuccessResponse(
                    student,
                    "Student created successfully."));
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,UpdateStudentRequest request)
        {
            var updated = await studentService.UpdateStudent(id, request);
            if (!updated)
                return NotFound(ApiResponse<string>.FailureResponse("Student not found."));
            return Ok(ApiResponse<string>.SuccessResponse(
                null,
                "Student updated successfully."));
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await studentService.DeleteStudent(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.FailureResponse("Student not found."));
            return Ok(ApiResponse<string>.SuccessResponse(
                null,
                "Student deleted successfully."));
        }
    }

}
