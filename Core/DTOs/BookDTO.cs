using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public int PublicationYear { get; set; }
        [Required]
        [MaxLength(13)]
        public string ISBN { get; set; } = string.Empty;
        [Required]
        public int Pages { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public AuthorDTO? Author { get; set; }
        public List<BookPublisherDTO> BookPublishers { get; set; } = new();
    }
}
