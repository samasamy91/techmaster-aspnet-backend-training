using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenter.Api.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int EnrollmentId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        [Required]
        [MaxLength(50)]
        public PaymentStatus PaymentStatus { get; set; }
        [Required]
        [MaxLength(100)]
        public string ReferenceNumber { get; set; }
        [MaxLength(500)]
        public string? Notes { get; set; }
        public Enrollment Enrollment { get; set; } = null!;


    }
}
