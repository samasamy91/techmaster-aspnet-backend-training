using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenter.Api.DTOs.Payments
{
    public class UpdatePaymentStatusRequest
    {
        public PaymentStatus PaymentStatus { get; set; }
    }
}
