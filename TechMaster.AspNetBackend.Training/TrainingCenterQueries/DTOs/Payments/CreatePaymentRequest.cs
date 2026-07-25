using System.ComponentModel.DataAnnotations;

namespace TrainingCenter.Api.DTOs.Payments
{
    public class CreatePaymentRequest
    {
        [Required]
        public int EnrollmentId { get; set; }
        [Range(0.01,double.MaxValue)]
        public decimal Amount { get; set; }
        [Required] 
        public string PaymentMethod { get; set; }
        public string? Notes { get; set; }
    }
}
