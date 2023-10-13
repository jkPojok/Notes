using Domain.Enums;

namespace Application.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string Gender { get; set; }
        public string? AboutMe { get; set; }
        public string? UserImageLink { get; set; }
        public string? RegistrationDate { get; set; } 
    }
}
