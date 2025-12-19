using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Streetcode.Auth.Domain.Entities
{
    [Table("Users", Schema = "Users")]
    public class User : IdentityUser<int>
    {
        /// <summary>
        /// Gets or sets the user's  name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Navigation property for refresh tokens.
        /// </summary>
        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
