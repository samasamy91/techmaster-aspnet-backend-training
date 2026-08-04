using TrainingCenter.Api.DTOs.Enrollments;
using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenter.Api.Services.IServices
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDetailsResponse>> GetAllEnrollments(string? status,
            int? trackId, int? studentId);
        Task<EnrollmentDetailsResponse?> GetEnrollmentById(int id);
        Task<EnrollmentDetailsResponse> CreateEnrollment(CreateEnrollmentRequest request);
        Task<bool> UpdateStatusEnrollment(int id, UpdateEnrollmentStatusRequest request);
        Task<IEnumerable<EnrollmentDetailsResponse>> GetStudentEnrollments(int studentId);
        Task<IEnumerable<EnrollmentDetailsResponse>> GetTrackStudents(int trackId);
    }
}
