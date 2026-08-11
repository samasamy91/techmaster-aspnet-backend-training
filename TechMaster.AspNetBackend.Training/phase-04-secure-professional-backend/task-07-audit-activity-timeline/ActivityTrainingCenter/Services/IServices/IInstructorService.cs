using SecurePlatformUpgrade.DTOs.Instructors;
using SecurePlatformUpgrade.DTOs.TrackSession;
using System.Security.Claims;
using TrainingCenter.Api.DTOs.Instructors;
using TrainingCenter.Api.DTOs.Tracks;

namespace TrainingCenter.Api.Services.IServices
{
    public interface IInstructorService
    {
        Task<IEnumerable<InstructorResponse>> GetAllInstructor();
        Task<InstructorResponse?> GetInstructorById(int id);
        Task<InstructorResponse> CreateInstructor(CreateInstructorRequest request);
        Task<bool> UpdateInstructor(int id, UpdateInstructorRequest request);
        Task<IEnumerable<TrackDetailsResponse>> GetTracksByInstructor(int instructorId);
        Task<TrackSessionResponse> CreateTrackSession(int trackId, CreateTrackSessionRequest request, ClaimsPrincipal user);
        Task<TrackSessionResponse?> UpdateTrackSession(int sessionId, UpdateTrackSessionRequest request, ClaimsPrincipal user);
        Task<TrackProgressResponse?> GetTrackProgress(int trackId, ClaimsPrincipal user);
    }
}
