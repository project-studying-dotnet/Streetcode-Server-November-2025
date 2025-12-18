using Streetcode.DAL.Enums;

namespace Streetcode.BLL.DTO.Users
{
    public class RegisterUserDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string? PhoneNumber { get; set; }

        public UserRole Role { get; set; }
    }
}
