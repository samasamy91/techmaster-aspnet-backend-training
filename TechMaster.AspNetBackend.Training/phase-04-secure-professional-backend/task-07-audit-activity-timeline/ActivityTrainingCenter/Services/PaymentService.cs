using ActivityTrainingCenter.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Payments;
using TrainingCenter.Api.Entities;
using TrainingCenter.Api.Entities.Enums;
using TrainingCenter.Api.Services.IServices;
using ValidationTrainingCenter.Common.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TrainingCenter.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext context;
        private readonly ILogger<PaymentService> logger;
        private readonly IActivityLogService logService;
        public PaymentService(AppDbContext context, ILogger<PaymentService> logger, IActivityLogService logService)
        {
            this.context = context;
            this.logger = logger;
            this.logService = logService;
        }
        private string GenerateReferenceNumber()
        {
            return $"PAY-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
        }
        public async Task<IEnumerable<PaymentResponse>> GetAllPayment(DateTime? fromDate, DateTime? toDate, PaymentStatus? status)
        {
            var query = context.Payments.AsQueryable();

            //var query = context.Payments.Where(p => p.PaymentDate > fromDate && p.PaymentDate < toDate && p.PaymentStatus == status).AsQueryable();
            if (fromDate.HasValue)
                query = query.Where(p => p.PaymentDate >= fromDate);

            if (toDate.HasValue)
                query = query.Where(p => p.PaymentDate <= toDate);

            if (status.HasValue)
                query = query.Where(p => p.PaymentStatus == status);
            return await query.OrderByDescending(p => p.PaymentDate).Select(p => new PaymentResponse
                {
                    PaymentId = p.PaymentId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    PaymentStatus = p.PaymentStatus,
                    ReferenceNumber = p.ReferenceNumber
                }).ToListAsync();
        }
        public async Task<object> GetStudentPayments(string email)
        {
            return await context.Payments.Where(p => p.Enrollment.Student.Email == email)
                .Select(p => new
                {
                    p.PaymentId,
                    p.Amount,
                    p.PaymentDate,
                    p.PaymentStatus,
                    Track = p.Enrollment.TrainingTrack.Title
                }).ToListAsync();
        }
        public async Task<PaymentResponse> CreatePayment(CreatePaymentRequest request, ClaimsPrincipal user)
        {
            var enrollment = await context.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentId == request.EnrollmentId);
            if (enrollment == null)
                throw new NotFoundException("Enrollment not found");

            if (request.Amount <= 0)
                throw new BusinessRuleException("Payment amount must be greater than zero");

            if (enrollment.Status == EnrollmentStatus.Cancelled)
                throw new BusinessRuleException("Cannot create payment for a cancelled enrollment.");

            var payment = new Payment
            {
                EnrollmentId = request.EnrollmentId,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = PaymentStatus.Pending,
                ReferenceNumber = GenerateReferenceNumber(),
                Notes = request.Notes
            };
            if (payment.PaymentStatus == PaymentStatus.Paid)
            {
                enrollment.Status = EnrollmentStatus.Active;
            }
            
            
            context.Payments.Add(payment);
            await context.SaveChangesAsync();

            await logService.Log(user, "Payment Created", "Payment", payment.PaymentId, $"Payment of {payment.Amount} was created for enrollment {payment.EnrollmentId}");

            return new PaymentResponse
            {
                PaymentId = payment.PaymentId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentStatus = payment.PaymentStatus,
                ReferenceNumber = payment.ReferenceNumber
            };
        }
        public async Task<IEnumerable<PaymentResponse>> GetEnrollmentPayments(int enrollmentId)
        {
            return await context.Payments.Where(p => p.EnrollmentId == enrollmentId).OrderByDescending(p => p.PaymentDate)
                .Select(p => new PaymentResponse
                {
                    PaymentId = p.PaymentId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    PaymentStatus = p.PaymentStatus,
                    ReferenceNumber = p.ReferenceNumber
                }).ToListAsync();
        }
        public async Task<bool> UpdateStatusPayment(int paymentId,UpdatePaymentStatusRequest request, ClaimsPrincipal user)
        {
            var payment = await context.Payments.Include(p=>p.Enrollment).FirstOrDefaultAsync(p => p.PaymentId == paymentId);
            if (payment == null)
                return false;
            var oldStatus = payment.PaymentStatus;
            payment.PaymentStatus = request.PaymentStatus;
            if (payment.PaymentStatus == PaymentStatus.Paid)
            {
                payment.Enrollment.Status = EnrollmentStatus.Active;
            }
            await context.SaveChangesAsync();

            await logService.Log(user, "Payment Status Updated", "Payment", payment.PaymentId, $"Payment status changed from {oldStatus} to {payment.PaymentStatus}",
                metadata: System.Text.Json.JsonSerializer.Serialize(new
                {
                    OldStatus = oldStatus,
                    NewStatus = payment.PaymentStatus.ToString()
                }));

            logger.LogInformation("Payment status updated , PaymentId: {PaymentId}, Status: {Status}", paymentId, request.PaymentStatus);
            return true;
        }
    }
}
