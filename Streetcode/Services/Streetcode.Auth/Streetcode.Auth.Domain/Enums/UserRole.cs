namespace Streetcode.Auth.Domain.Enums;

/// <summary>
/// User roles in the system.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Regular user with limited access.
    /// </summary>
    User = 0,

    /// <summary>
    /// Administrator with full access.
    /// </summary>
    Admin = 1
}