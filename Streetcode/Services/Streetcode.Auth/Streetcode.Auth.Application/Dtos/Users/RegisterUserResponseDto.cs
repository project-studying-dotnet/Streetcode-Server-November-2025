using Streetcode.Auth.Domain.Enums;

namespace Streetcode.Auth.Application.Dtos.Users
{
    public class RegisterUserResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string? PhoneNumber { get; set; }

        public UserRole Role { get; set; }
    }
}
