using SecurePlatformUpgrade.DTOs.Students;
using System.Security.Claims;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.DTOs.Students;

namespace TrainingCenter.Api.Services.IServices
{
    public interface IStudentService
    {
        Task<PaginationResult<StudentListItemResponse>> GetAllStudent(string? search,
            bool? isActive, int pageNumber, int pageSize);
        Task<StudentDetailsResponse?> GetStudentById(int id);
        Task<StudentDetailsResponse?> GetCurrentStudent(string email);
        Task<StudentDetailsResponse> CreateStudent(CreateStudentRequest request);
        Task<bool> UpdateStudent(int id, UpdateStudentRequest request);
        Task<bool> DeleteStudent(int id);
        Task<object?> UpdateMyProfile(UpdateMyStudentProfile request, ClaimsPrincipal user);
    }
}
