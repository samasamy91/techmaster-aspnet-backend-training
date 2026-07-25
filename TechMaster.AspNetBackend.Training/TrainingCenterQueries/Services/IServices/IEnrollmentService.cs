using TrainingCenter.Api.DTOs.Enrollments;
using TrainingCenter.Api.Entities.Enums;
using TrainingCenterQueries.DTOs.Enrollments;

namespace TrainingCenter.Api.Services.IServices
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentListItemResponse>> GetAllEnrollments(string? status, int? trackId, int? studentId);
        Task<EnrollmentDetailsResponse?> GetEnrollmentById(int id);
        Task<EnrollmentDetailsResponse> CreateEnrollment(CreateEnrollmentRequest request);
        Task<bool> UpdateStatusEnrollment(int id, UpdateEnrollmentStatusRequest request);
        Task<IEnumerable<StudentEnrollmentHistoryResponse>> GetStudentEnrollments(int studentId);
        Task<IEnumerable<TrackStudentResponse>> GetTrackStudents(int trackId);
    }
}
