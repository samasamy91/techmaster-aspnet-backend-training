using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.Services.IServices;

namespace TrainingCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService service;

        public ReportController(IReportService service)
        {
            this.service = service;
        }

        [HttpGet("dashboard-summary")]
        public async Task<IActionResult> DashboardSummary()
        {
            var result = await service.GetDashboardSummary();

            return Ok(ApiResponse<object>.SuccessResponse(
                result,
                "Dashboard summary retrieved successfully."));
        }

        [HttpGet("unpaid-enrollments")]
        public async Task<IActionResult> UnpaidEnrollments()
        {
            var result = await service.GetUnpaidEnrollments();

            return Ok(ApiResponse<object>.SuccessResponse(
                result,
                "Unpaid enrollments retrieved successfully."));
        }

        [HttpGet("track-capacity")]
        public async Task<IActionResult> TrackCapacity()
        {
            var result = await service.GetTrackCapacity();

            return Ok(ApiResponse<object>.SuccessResponse(
                result,
                "Track capacity report retrieved successfully."));
        }

        [HttpGet("revenue-summary")]
        public async Task<IActionResult> RevenueSummary()
        {
            var result = await service.GetRevenueSummary();

            return Ok(ApiResponse<object>.SuccessResponse(
                result,
                "Revenue summary retrieved successfully."));
        }

        [HttpGet("revenue-by-track")]
        public async Task<IActionResult> RevenueByTrack()
        {
            var result = await service.GetRevenueByTrack();

            return Ok(ApiResponse<object>.SuccessResponse(
                result,
                "Revenue by track retrieved successfully."));
        }
        //Query 7
        [HttpGet("tracks-with-available-seats")]
        public async Task<IActionResult> GetTracksWithAvailableSeats()
        {
            var result = await service.GetTracksWithAvailSeats();

            return Ok(ApiResponse<object>.SuccessResponse(
                result,
                "Tracks with available seats retrieved successfully."));
        }
        //Query 16
        [HttpGet("top-tracks")]
        public async Task<IActionResult> GetTopTracks([FromQuery] int top = 5)
        {
            var result = await service.GetTopTracksAsync(top);

            return Ok(ApiResponse<object>.SuccessResponse(
                result,
                "Top tracks retrieved successfully."));
        }
        //Query 17
        [HttpGet("instructor-workload")]
        public async Task<IActionResult> GetInstructorWorkload()
        {
            var result = await service.GetInstructorWorkload();

            return Ok(ApiResponse<object>.SuccessResponse(
                result,
                "Instructor workload retrieved successfully."));
        }
        //Query 18
        [HttpGet("students-without-payments")]
        public async Task<IActionResult> GetStudentsWithoutPayments()
        {
            var result = await service.GetStudentsWithoutPayments();

            return Ok(ApiResponse<object>.SuccessResponse(
                result,
                "Students without payments retrieved successfully."));
        }
    }
}
