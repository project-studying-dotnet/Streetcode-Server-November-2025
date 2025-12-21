namespace Streetcode.Auth.Domain.Events;

/// <summary>
/// Event raised when a user successfully logs in.
/// </summary>
public record UserLoggedInEvent(
    int UserId,
    string Email,
    DateTime LoggedInAt);