using Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class ChangePasswordUserDto
    {
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [NotEqual("CurrentPassword")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
