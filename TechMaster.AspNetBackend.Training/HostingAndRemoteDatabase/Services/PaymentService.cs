using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Payments;
using TrainingCenter.Api.Entities;
using TrainingCenter.Api.Entities.Enums;
using TrainingCenter.Api.Services.IServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TrainingCenter.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext context;
        public PaymentService(AppDbContext context)
        {
            this.context = context;
        }
        private string GenerateReferenceNumber()
        {
            return $"PAY-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
        }
        public async Task<IEnumerable<PaymentResponse>> GetAllPayment(DateTime? fromDate, DateTime? toDate, PaymentStatus? status)
        {
            var query = context.Payments.Where(p => p.PaymentDate > fromDate && p.PaymentDate < toDate && p.PaymentStatus == status).AsQueryable();
            if (fromDate.HasValue)
                query = query.Where(p => p.PaymentDate >= fromDate);

            if (toDate.HasValue)
                query = query.Where(p => p.PaymentDate <= toDate);

            if (status.HasValue)
                query = query.Where(p => p.PaymentStatus == status);
            return await context.Payments.OrderByDescending(p => p.PaymentDate).Select(p => new PaymentResponse
                {
                    PaymentId = p.PaymentId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    PaymentStatus = p.PaymentStatus,
                    ReferenceNumber = p.ReferenceNumber
                }).ToListAsync();
        }
        public async Task<PaymentResponse> CreatePayment(CreatePaymentRequest request)
        {
            var enrollment = await context.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentId == request.EnrollmentId);
            if (enrollment == null)
                throw new Exception("Enrollment nor found");
            var payment = new Payment
            {
                EnrollmentId = request.EnrollmentId,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = Entities.Enums.PaymentStatus.Pending,
                ReferenceNumber = GenerateReferenceNumber(),
                Notes = request.Notes
            };
            context.Payments.Add(payment);
            await context.SaveChangesAsync();
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
        public async Task<bool> UpdateStatusPayment(int paymentId,UpdatePaymentStatusRequest request)
        {
            var payment = await context.Payments.FirstOrDefaultAsync(p => p.PaymentId == paymentId);
            if (payment == null)
                return false;
            payment.PaymentStatus = request.PaymentStatus;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
