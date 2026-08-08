using System.Security.Claims;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.DTOs.Tracks;
using TrainingCenter.Api.Entities.Enums;

namespace TrainingCenter.Api.Services.IServices
{
    public interface ITrackService
    {
        Task<PaginationResult<TrackDetailsResponse>> GetAllTracks(string? keyword,
            string? level, string? status, int? instructorId, int pageNumber, int pageSize);
        Task<TrackDetailsResponse?> GetTrackById(int id);
        Task<object?> GetAvailableTracks();
        Task<object> GetInstructorTracks(string email);
        Task<TrackDetailsResponse> CreateTrack(CreateTrackRequest request);
        Task<bool> UpdateTrack(int id, UpdateTrackRequest request);
        Task<bool> DeleteTrack(int id);
        Task<object?> GetTrackStudents(int trackId, ClaimsPrincipal user);
    }
}
