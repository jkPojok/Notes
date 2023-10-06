using Domain.Enums;

namespace Application.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public Gender Gender { get; set; }
        public string? UserImageLink { get; set; }
        public DateTime RegistrationDate { get; set; } 
    }
}
