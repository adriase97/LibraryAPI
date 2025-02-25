using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class PublisherDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;
        public int FoundedYear { get; set; }
        [Required]
        [MaxLength(255)]
        public string Website { get; set; } = string.Empty;
        public List<BookPublisherDTO> BookPublishers { get; set; } = new();
    }
}
