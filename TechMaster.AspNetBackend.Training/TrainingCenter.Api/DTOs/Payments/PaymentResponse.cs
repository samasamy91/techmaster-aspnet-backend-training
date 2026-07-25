using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenter.Api.DTOs.Payments
{
    public class PaymentResponse
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
