namespace RefactoredEnrollment.DTOs
{
    public class PaymentResponseDto
    {
        public int PaymentId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentStatus { get; set; }

        public string ReferenceNumber { get; set; } 
    }
}
