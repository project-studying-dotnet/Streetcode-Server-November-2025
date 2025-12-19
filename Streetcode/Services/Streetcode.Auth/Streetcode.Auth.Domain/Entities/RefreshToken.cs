namespace Streetcode.Auth.Domain.Entities;

/// <summary>
/// Refresh token entity for JWT token rotation.
/// </summary>
public class RefreshToken
{
    /// <summary>
    /// Gets or sets the token identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the user ID this token belongs to.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the token string.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the JWT token ID (jti claim).
    /// </summary>
    public string JwtId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets when the token was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets when the token expires.
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Gets or sets whether the token has been used.
    /// </summary>
    public bool IsUsed { get; set; }

    /// <summary>
    /// Gets or sets whether the token has been revoked.
    /// </summary>
    public bool IsRevoked { get; set; }

    /// <summary>
    /// Gets whether the token is expired.
    /// </summary>
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    /// <summary>
    /// Gets whether the token is valid (not used, not revoked, not expired).
    /// </summary>
    public bool IsValid => !IsUsed && !IsRevoked && !IsExpired;

    /// <summary>
    /// Navigation property to user.
    /// </summary>
    public virtual User User { get; set; } = null!;
}