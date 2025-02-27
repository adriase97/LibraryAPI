using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class ReplaceClaimDTO
    {
        [Required]
        public string OldType { get; set; } = string.Empty;
        [Required]
        public string OldValue { get; set; } = string.Empty;
        [Required]
        public string NewType { get; set; } = string.Empty;
        [Required]
        public string NewValue { get; set; } = string.Empty;
    }
}
