using System.ComponentModel.DataAnnotations;

namespace NotesIdentity.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Не указано имя")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Не указана электронная почта")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string? PasswordConfirmation { get; set; }
    }
}
