using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Nationality { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Biography { get; set; } = string.Empty;
        public List<BookDTO> Books { get; set; } = new();
    }
}
