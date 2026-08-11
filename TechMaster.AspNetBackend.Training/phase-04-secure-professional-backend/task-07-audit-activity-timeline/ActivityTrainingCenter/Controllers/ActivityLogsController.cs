using ActivityTrainingCenter.DTOs.ActivityLogs;
using ActivityTrainingCenter.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Api.Common;

namespace ActivityTrainingCenter.Controllers
{
    [Route("api/admin/activity-logs")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class ActivityLogsController : ControllerBase
    {
        private readonly IActivityLogService logService;
        public ActivityLogsController(IActivityLogService logService)
        {
            this.logService = logService;
        }
        [HttpGet]
        public async Task<IActionResult> GetActivityLogs([FromQuery] ActivityLogQueryDTO query)
        {
            var result = await logService.GetLogs(query);
            return Ok(ApiResponse<object>.SuccessResponse(result, "Retrieved successfully"));
        }
    }
}
