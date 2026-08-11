using TrainingCenter.Api.Common;

namespace ActivityTrainingCenter.DTOs.ActivityLogs
{
    public class ActivityLogQueryDTO : PagedRequest
    {
        public int? UserId { get; set; }
        public string? EntityName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
