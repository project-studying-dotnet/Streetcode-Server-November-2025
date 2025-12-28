using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Streetcode.Auth.Domain.Entities.Auth;

namespace Streetcode.Auth.Domain.Entities.Users
{
    [Table("Users", Schema = "Users")]
    public class User : IdentityUser<int>
    {
        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
