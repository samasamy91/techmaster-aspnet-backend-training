using TrainingCenter.Api.Entities.Enums;

namespace RefactoredEnrollment.DTOs.Enrollments
{
    public class PaymentResponse
    {
        public int Id { get; set; }
        public int EnrollmentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
