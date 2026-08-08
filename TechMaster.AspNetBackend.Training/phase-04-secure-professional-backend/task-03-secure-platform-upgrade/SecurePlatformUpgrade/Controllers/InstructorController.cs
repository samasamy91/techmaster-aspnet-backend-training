using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurePlatformUpgrade.DTOs.TrackSession;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.DTOs.Instructors;
using TrainingCenter.Api.Services.IServices;

namespace TrainingCenter.Api.Controllers
{
    [Route("api/instructors")]
    [ApiController]
    
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService service;

        public InstructorController(IInstructorService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(ApiResponse<object>.SuccessResponse(
                await service.GetAllInstructor()));
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            var instructor = await service.GetInstructorById(id);
            if (instructor == null)
                return NotFound(ApiResponse<string>.FailureResponse("Instructor not found."));

            return Ok(ApiResponse<object>.SuccessResponse(instructor));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateInstructorRequest request)
        {
            var instructor = await service.CreateInstructor(request);
            return CreatedAtAction(
                nameof(Get),
                new { id = instructor.InstructorId },
                ApiResponse<object>.SuccessResponse(instructor));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id,UpdateInstructorRequest request)
        {
            if (!await service.UpdateInstructor(id, request))
                return NotFound(ApiResponse<string>.FailureResponse("Instructor not found."));

            return Ok(ApiResponse<string>.SuccessResponse(null, "Instructor updated."));
        }

        [HttpGet("{id:int}/tracks")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Tracks(int id)
        {
            return Ok(ApiResponse<object>.SuccessResponse(
                await service.GetTracksByInstructor(id)));
        }
        [HttpPost("tracks/{id:int}/sessions")]
        [Authorize(Roles ="Instructor")]
        public async Task<IActionResult> CreateSession(int id, CreateTrackSessionRequest request)
        {
            try
            {
                var result = await service.CreateTrackSession(id, request, User);
                return Created($"api/instructor/tracks/{id}/sessions/{result.TrackSessionId}",
                    ApiResponse<object>.SuccessResponse(result, "Session Created successfully"));
            }catch(UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, ApiResponse<string>.FailureResponse(ex.Message));
            }catch(BadHttpRequestException ex)
            {
                return BadRequest(ApiResponse<string>.FailureResponse(ex.Message));
            }
        }
        [HttpPut("sessions/{id:int}")]
        [Authorize(Roles ="Instructor")]
        public async Task<IActionResult> UpdateSession(int id,UpdateTrackSessionRequest request)
        {
            try
            {
                var result = await service.UpdateTrackSession(id, request, User);
                if (result == null)
                    return NotFound(ApiResponse<string>.FailureResponse("Session not found"));
                return Ok(ApiResponse<object>.SuccessResponse(result, "Session Updated successfully"));
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, ApiResponse<string>.FailureResponse(ex.Message));
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ApiResponse<string>.FailureResponse(ex.Message));
            }
        }
        [HttpGet("tracks/{id:int}/progress")]
        [Authorize(Roles ="Instructor")]
        public async Task<IActionResult> GetTrackProgress(int id)
        {
            try
            {
                var result = await service.GetTrackProgress(id,User);
                if (result == null)
                    return NotFound(ApiResponse<string>.FailureResponse("Track not found"));
                return Ok(ApiResponse<object>.SuccessResponse(result, "Track progress retrieved successfully"));
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, ApiResponse<string>.FailureResponse(ex.Message));
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ApiResponse<string>.FailureResponse(ex.Message));
            }
        }
    }
}
