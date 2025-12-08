using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Streetcode.DAL.Enums;

namespace Streetcode.DAL.Entities.Users
{
    [Table("Users", Schema = "Users")]
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        public UserRole Role { get; set; }
    }
}
