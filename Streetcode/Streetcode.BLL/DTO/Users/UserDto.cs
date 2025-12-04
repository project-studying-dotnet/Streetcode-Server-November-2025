using System.ComponentModel.DataAnnotations;
using Streetcode.DAL.Enums;

namespace Streetcode.BLL.DTO.Users
{
    // This Dto intentionally omits validation attributes (e.g., [Required], [MaxLength])
    // Validation for this DTO must be implemented exclusively in a FluentValidation class
    // to adhere to the Separation of Concerns principle.
    public class UserDto
    {
        public int Id { get; set; }

        // [Required]
        // [MaxLength(50)]
        public string Name { get; set; }

        // [Required]
        // [MaxLength(50)]
        public string Surname { get; set; }

        // [Required]
        // [EmailAddress]
        public string Email { get; set; }

        // [Required]
        // [MaxLength(20)]
        public string Login { get; set; }

        // [Required]
        // [MaxLength(20)]
        public string Password { get; set; }

        // [Required]
        public UserRole Role { get; set; }
    }
}
