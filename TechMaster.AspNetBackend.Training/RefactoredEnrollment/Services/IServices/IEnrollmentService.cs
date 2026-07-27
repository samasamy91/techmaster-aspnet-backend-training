using RefactoredEnrollment.DTOs.Enrollments;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenter.Api.Services.IServices
{
    public interface IEnrollmentService
    {
        Task<PaginationResult<EnrollmentList>> GetAll(int page, int pageSize);
        Task<EnrollmentResponse> Create(CreateEnrollmentRequest request);
    }
}
