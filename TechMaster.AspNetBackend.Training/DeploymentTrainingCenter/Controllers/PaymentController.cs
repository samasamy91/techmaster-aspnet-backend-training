using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.DTOs.Payments;
using TrainingCenter.Api.Entities.Enums;
using TrainingCenter.Api.Services.IServices;

namespace TrainingCenter.Api.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService service;

        public PaymentController(IPaymentService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery]DateTime? fromDate, [FromQuery] DateTime? toDate, PaymentStatus? status)
        {
            var payments = await service.GetAllPayment(fromDate,toDate,status);

            return Ok(ApiResponse<object>.SuccessResponse(
                payments,
                "Payments retrieved successfully."));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreatePaymentRequest request)
        {
            var payment = await service.CreatePayment(request);

            return Created(
                $"api/payments/{payment.PaymentId}",
                ApiResponse<object>.SuccessResponse(
                    payment, "Payment created successfully."));
        }

        [HttpGet("enrollments/{id:int}/payments")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEnrollmentPayments(int id)
        {
            var payments = await service.GetEnrollmentPayments(id);

            return Ok(ApiResponse<object>.SuccessResponse(
                payments,
                "Payment history retrieved successfully."));
        }

        [HttpPut("{id:int}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            UpdatePaymentStatusRequest request)
        {
            var updated = await service.UpdateStatusPayment(id, request);

            if (!updated)
            {
                return NotFound(
                    ApiResponse<string>.FailureResponse("Payment not found."));
            }

            return Ok(ApiResponse<string>.SuccessResponse("Payment status updated successfully."));
        }
        [HttpGet("my-payments")]
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> GetStudentPayment()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
                return Unauthorized(ApiResponse<string>.FailureResponse("Not authorized"));
            var payments = await service.GetStudentPayments(email!);
            if (payments == null)
                return Ok(ApiResponse<string>.SuccessResponse("No enrollments yet"));
            return Ok(ApiResponse<object>.SuccessResponse(payments, "Enrollments retrieved successfully"));
        }
    }
}
