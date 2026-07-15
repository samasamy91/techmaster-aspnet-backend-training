using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.DTOs
{
    public class CreateAuthorRequest
    {
        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
