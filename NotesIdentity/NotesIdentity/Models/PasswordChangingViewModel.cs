using System.ComponentModel.DataAnnotations;

namespace NotesIdentity.Models
{
    public class PasswordChangingViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string? NewPasswordConfiramtion { get; set; }
    }
}
