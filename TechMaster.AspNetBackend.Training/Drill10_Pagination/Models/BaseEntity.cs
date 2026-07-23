namespace Drill08_AuditFields.Models
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
