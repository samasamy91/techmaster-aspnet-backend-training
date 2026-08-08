using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.DTOs.Instructors;
using TrainingCenter.Api.Services.IServices;

namespace TrainingCenter.Api.Controllers
{
    [Route("api/instructors")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService service;

        public InstructorController(IInstructorService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(ApiResponse<object>.SuccessResponse(
                await service.GetAllInstructor()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var instructor = await service.GetInstructorById(id);
            if (instructor == null)
                return NotFound(ApiResponse<string>.FailureResponse("Instructor not found."));

            return Ok(ApiResponse<object>.SuccessResponse(instructor));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInstructorRequest request)
        {
            var instructor = await service.CreateInstructor(request);
            return CreatedAtAction(
                nameof(Get),
                new { id = instructor.InstructorId },
                ApiResponse<object>.SuccessResponse(instructor));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,UpdateInstructorRequest request)
        {
            if (!await service.UpdateInstructor(id, request))
                return NotFound(ApiResponse<string>.FailureResponse("Instructor not found."));

            return Ok(ApiResponse<string>.SuccessResponse(null, "Instructor updated."));
        }

        [HttpGet("{id:int}/tracks")]
        public async Task<IActionResult> Tracks(int id)
        {
            return Ok(ApiResponse<object>.SuccessResponse(
                await service.GetTracksByInstructor(id)));
        }
    }
}
