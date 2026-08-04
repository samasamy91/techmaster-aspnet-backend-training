using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefactoredEnrollment.DTOs.Enrollments;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.Services;
using TrainingCenter.Api.Services.IServices;

namespace TrainingCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService service;
        public EnrollmentController(IEnrollmentService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var result = await service.GetAll(page, pageSize);
            return Ok(ApiResponse<object>.SuccessResponse(result, "Enrollments retrieved successfully"));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEnrollmentRequest request)
        {
            try
            {
                var enrollment = await service.Create(request);
                return CreatedAtAction(nameof(GetAll), new { id = enrollment.Id },
                    ApiResponse<object>.SuccessResponse(enrollment, "Enrollment created successfully"));
            }catch(KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<string>.FailureResponse(ex.Message));
            }
            catch(BadHttpRequestException ex)
            {
                return BadRequest(ApiResponse<string>.FailureResponse(ex.Message));
            }
        }
        [HttpPost("{enrollmentId}/payment")]
        public async Task<IActionResult> Pay(int enrollmentId, PaymentRequest request)
        {
            try
            {
                var payment = await service.Pay(enrollmentId, request.Amount);
                return Ok(ApiResponse<object>.SuccessResponse(payment, "Payment processed successfully"));
            }catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<string>.FailureResponse(ex.Message));
            }catch(BadHttpRequestException ex)
            {
                return BadRequest(ApiResponse<string>.FailureResponse(ex.Message));
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await service.Delete(id);
            if (!deleted)
            {
                return NotFound(ApiResponse<string>.FailureResponse("Enrollment not found"));
            }
            return Ok(ApiResponse<string>.SuccessResponse("Enrollment deleted successfully"));
        }
    }
}
