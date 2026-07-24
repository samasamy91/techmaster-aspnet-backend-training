namespace RefactoredEnrollment.DTOs
{
    public class CreatePaymentRequest
    {
        public int EnrollmentId { get; set; }

        public decimal Amount { get; set; }
    }
}
