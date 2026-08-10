using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurePlatformUpgrade.DTOs.Enrollments;
using System.Security.Claims;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.DTOs.Enrollments;
using TrainingCenter.Api.Services;
using TrainingCenter.Api.Services.IServices;

namespace TrainingCenter.Api.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService service;
        public EnrollmentController(IEnrollmentService service)
        {
            this.service = service;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery]string? status, [FromQuery]int? trackId, [FromQuery]int? studentId)
        {
            var result = await service.GetAllEnrollments(status, trackId, studentId);
            return Ok(ApiResponse<object>.SuccessResponse(result, "Enrollments retrieved successfully"));
        }
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var enrollment = await service.GetEnrollmentById(id);
            if(enrollment == null)
            {
                return NotFound(ApiResponse<string>.FailureResponse("Enrollment not found"));
            }
            return Ok(ApiResponse<object>.SuccessResponse(enrollment, "Enrollment retrieved successfully"));

        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateEnrollmentRequest request)
        {
            var enrollment = await service.CreateEnrollment(request);
            return CreatedAtAction(nameof(GetById), new { id = enrollment.EnrollmentId },
                ApiResponse<object>.SuccessResponse(enrollment, "Student enrolled successfully"));
        }
        [HttpPut("{id:int}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int id,UpdateEnrollmentStatusRequest request)
        {
            var updated = await service.UpdateStatusEnrollment(id, request);
            if (!updated)
            {
                return NotFound(ApiResponse<string>.FailureResponse("Enrollment not found"));
            }
            return Ok(ApiResponse<string>.SuccessResponse(null, "Enrollment status updated successfully"));
        }
        [HttpGet("students/{id:int}/enrollments")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetStudentEnrollments(int id)
        {
            var result = await service.GetStudentEnrollments(id);
            return Ok(ApiResponse<object>.SuccessResponse(result, "Student enrollment history successfully"));
        }
        [HttpGet("tracks/{id:int}/students")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTrackStudents(int id)
        {
            var result = await service.GetTrackStudents(id);
            return Ok(ApiResponse<object>.SuccessResponse(result, "Track students retrieved successfully"));
        }
        [HttpGet("my-enrollments")]
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> GetStudentEnrollment()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
                return Unauthorized(ApiResponse<string>.FailureResponse("Not authorized"));
            var enrollments = await service.GetStudentEnrollment(email!);
            if (enrollments == null)
                return Ok(ApiResponse<string>.SuccessResponse("No enrollments yet"));
            return Ok(ApiResponse<object>.SuccessResponse(enrollments, "Enrollments retrieved successfully"));
        }
        [HttpGet("my-track-enrollments")]
        [Authorize(Roles ="Instructor")]
        public async Task<IActionResult> GetInstructorEnrollment()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
                return Unauthorized(ApiResponse<string>.FailureResponse("Not authorized"));
            var enrollments = await service.GetInstructorEnrollments(email!);
            if (enrollments == null)
                return Ok(ApiResponse<string>.SuccessResponse("No enrollments yet"));
            return Ok(ApiResponse<object>.SuccessResponse(enrollments, "Enrollments retrieved successfully"));
        }
        [HttpPost("enrollment-requests")]
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> RequestEnrollment(StudentEnrollmentRequest request)
        {
           
            var result = await service.RequestEnrollment(request, User);
            return Ok(ApiResponse<object>.SuccessResponse(result, "Enrollment request submitted successfully"));
            
        }
    }
}
