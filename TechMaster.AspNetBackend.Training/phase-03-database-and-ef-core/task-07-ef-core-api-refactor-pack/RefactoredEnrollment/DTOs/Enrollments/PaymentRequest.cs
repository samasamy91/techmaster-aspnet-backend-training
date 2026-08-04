using System.ComponentModel.DataAnnotations;

namespace RefactoredEnrollment.DTOs.Enrollments
{
    public class PaymentRequest
    {
        [Range(0.01,double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
