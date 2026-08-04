using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.DTOs.Payments;
using TrainingCenter.Api.Entities.Enums;
using TrainingCenter.Api.Services.IServices;

namespace TrainingCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService service;

        public PaymentController(IPaymentService service)
        {
            this.service = service;
        }

        [HttpGet("payments")]
        public async Task<IActionResult> GetAll([FromQuery]DateTime? fromDate, [FromQuery] DateTime? toDate, string? status)
        {
            var payments = await service.GetAllPayment(fromDate,toDate,status);

            return Ok(ApiResponse<object>.SuccessResponse(
                payments,
                "Payments retrieved successfully."));
        }

        [HttpPost("payments")]
        public async Task<IActionResult> Create(CreatePaymentRequest request)
        {
            var payment = await service.CreatePayment(request);

            return Created(
                $"api/payments/{payment.PaymentId}",
                ApiResponse<object>.SuccessResponse(
                    payment,
                    "Payment created successfully."));
        }

        [HttpGet("enrollments/{id:int}/payments")]
        public async Task<IActionResult> GetEnrollmentPayments(int id)
        {
            var payments = await service.GetEnrollmentPayments(id);

            return Ok(ApiResponse<object>.SuccessResponse(
                payments,
                "Payment history retrieved successfully."));
        }

        [HttpPut("payments/{id:int}/status")]
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

            return Ok(ApiResponse<string>.SuccessResponse(
                null,
                "Payment status updated successfully."));
        }
    }
}
