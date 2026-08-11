using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.DTOs.Tracks;
using TrainingCenter.Api.Services.IServices;
using TrainingCenterAuthorization.DTOs.Tracks;

namespace TrainingCenter.Api.Controllers
{
    [Route("api/tracks")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService service;
        private readonly IEnrollmentService enrollmentService;

        public TrackController(ITrackService service, IEnrollmentService enrollmentService)
        {
            this.service = service;
            this.enrollmentService = enrollmentService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(
            string? keyword,
            string? level,
            string? status,
            int? instructorId,
            [FromQuery] PagedRequest request)
        {
            return Ok(ApiResponse<object>.SuccessResponse(
                await service.GetAllTracks(keyword,level, status,instructorId, request.PageNumber, request.PageSize)));
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            var track = await service.GetTrackById(id);
            if (track == null)
                return NotFound(ApiResponse<string>.FailureResponse("Track not found."));
            return Ok(ApiResponse<object>.SuccessResponse(track));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateTrackRequest request)
        {
            var track = await service.CreateTrack(request, User);

            return CreatedAtAction(
                nameof(Get),
                new { id = track.TrainingTrackId },
                ApiResponse<object>.SuccessResponse(track));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            int id,
            UpdateTrackRequest request)
        {
            if (!await service.UpdateTrack(id, request))
                return NotFound(ApiResponse<string>.FailureResponse("Track not found."));

            return Ok(ApiResponse<string>.SuccessResponse(null, "Track updated."));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await service.DeleteTrack(id))
                return NotFound(ApiResponse<string>.FailureResponse("Track not found."));

            return Ok(ApiResponse<string>.SuccessResponse(null, "Track deleted."));
        }
        [HttpGet("available")]
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> GetAvailableTracks()
        {
            var result = await service.GetAvailableTracks();
            if (result == null)
                return Ok(ApiResponse<string>.SuccessResponse("No tracks yet"));
            return Ok(ApiResponse<object>.SuccessResponse(result, "Retrieved Successfully"));
        }
        [HttpGet("my-track")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> GetInstructorTracks()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
                return Unauthorized(ApiResponse<string>.FailureResponse("Not authorized"));
            var tracks = await service.GetInstructorTracks(email!);
            if (tracks == null)
                return Ok(ApiResponse<string>.SuccessResponse("No tracks yet"));
            return Ok(ApiResponse<object>.SuccessResponse(tracks, "Tracks retrieved successfully"));

        }
        [HttpGet("{id:int}/students")]
        [Authorize(Roles ="Admin,Instructor")]
        public async Task<IActionResult> GetTracksStudents(int id)
        {
            var result = await service.GetTrackStudents(id, User);
            if (result == null)
                return Forbid();//NotFound(ApiResponse<string>.FailureResponse("Track not found or you are not linked to an instructor"));
            return Ok(ApiResponse<object>.SuccessResponse(result, "Track students retrieved successfully"));
           
        }
        [HttpPut("{id:int}/assign-instructor")]
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> AssignInstructor(int id, AssignInstructorRequest request)
        {
            var result = await service.AssignInstructor(id, request.InstructorId);
            if (!result)
                return NotFound(ApiResponse<string>.FailureResponse("Track or instructor not found"));
            return Ok(ApiResponse<string>.SuccessResponse(null, "Instructor assigned to track successfully"));
        }
    }
}
