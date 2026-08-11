namespace ActivityTrainingCenter.DTOs.ActivityLogs
{
    public class ActivityLogDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? UserRole { get; set; }
        public string Action { get; set; }
        public string EntityName { get; set; }
        public int? EntityId { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? IpAddress { get; set; }
        public string? Metadata { get; set; }
    }
}
