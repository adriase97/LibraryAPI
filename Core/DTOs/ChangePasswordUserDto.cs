using Core.Validations;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class ChangePasswordUserDto
    {
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [NotEqual(nameof(CurrentPassword))]
        public string NewPassword { get; set; } = string.Empty;
    }
}
