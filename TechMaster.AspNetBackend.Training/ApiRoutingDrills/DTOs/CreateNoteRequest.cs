using System.ComponentModel.DataAnnotations;

namespace ApiRoutingDrills.DTOs
{
    public class CreateNoteRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
    }
}
