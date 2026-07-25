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
    }
}
