namespace Streetcode.Auth.Domain.Events;

/// <summary>
/// Event raised when a new user is registered.
/// </summary>
public record UserRegisteredEvent(
    int UserId,
    string Email,
    string Name,
    string Surname,
    DateTime RegisteredAt);