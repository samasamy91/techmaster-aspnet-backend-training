using TrainingCenter.Api.DTOs.Payments;
using TrainingCenter.Api.Entities;
using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenter.Api.Services.IServices
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentResponse>> GetAllPayment(DateTime? fromDate,DateTime? toDate,PaymentStatus? status);
        Task<PaymentResponse> CreatePayment(CreatePaymentRequest request);
        Task<IEnumerable<PaymentResponse>> GetEnrollmentPayments(int enrollmentId);
        Task<bool> UpdateStatusPayment(int paymentId, UpdatePaymentStatusRequest request);
    }
}
