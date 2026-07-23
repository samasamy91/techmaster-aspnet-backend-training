using Drill04_ManyToManyEnrollment.Models;

namespace Drill05_PaymentSummary.Models
{
    public class PaymentSummary
    {
        public int Id { get; set; }

        public int EnrollmentId { get; set; }

        public Enrollment? Enrollment { get; set; }

        public decimal TotalRequired { get; set; }

        public decimal TotalPaid { get; set; }

        public decimal RemainingAmount => TotalRequired - TotalPaid;

        public PaymentStatus PaymentStatus { get; set; }
    }
}
