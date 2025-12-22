using Streetcode.Auth.Domain.Entities.Users;

namespace Streetcode.Auth.Domain.Entities.Auth;

/// <summary>
/// Refresh token entity for JWT token rotation.
/// </summary>
public class RefreshToken
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public DateTime ExpiresOn { get; set; }

    public bool IsRevoked { get; set; }
}