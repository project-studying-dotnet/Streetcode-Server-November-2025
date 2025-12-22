using Microsoft.AspNetCore.Identity;
using Streetcode.Auth.Domain.Entities.Auth;

namespace Streetcode.Auth.Domain.Entities.Users
{
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
