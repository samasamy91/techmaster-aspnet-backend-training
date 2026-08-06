using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.DTOs.Tracks;
using TrainingCenter.Api.Services.IServices;

namespace TrainingCenter.Api.Controllers
{
    [Route("api/tracks")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService service;

        public TrackController(ITrackService service)
        {
            this.service = service;
        }

        [HttpGet]
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
        public async Task<IActionResult> Get(int id)
        {
            var track = await service.GetTrackById(id);
            if (track == null)
                return NotFound(ApiResponse<string>.FailureResponse("Track not found."));
            return Ok(ApiResponse<object>.SuccessResponse(track));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTrackRequest request)
        {
            var track = await service.CreateTrack(request);

            return CreatedAtAction(
                nameof(Get),
                new { id = track.TrainingTrackId },
                ApiResponse<object>.SuccessResponse(track));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateTrackRequest request)
        {
            if (!await service.UpdateTrack(id, request))
                return NotFound(ApiResponse<string>.FailureResponse("Track not found."));

            return Ok(ApiResponse<string>.SuccessResponse(null, "Track updated."));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await service.DeleteTrack(id))
                return NotFound(ApiResponse<string>.FailureResponse("Track not found."));

            return Ok(ApiResponse<string>.SuccessResponse(null, "Track deleted."));
        }
    }
}
