using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenter.Api.DTOs.Enrollments
{
    public class UpdateEnrollmentStatusRequest
    {
        public EnrollmentStatus Status { get; set; }
    }
}
