namespace Streetcode.Auth.Domain.Exceptions;

/// <summary>
/// Exception thrown when attempting to register with an existing email.
/// </summary>
public class UserAlreadyExistsException : DomainException
{
    public UserAlreadyExistsException(string email)
        : base($"User with email '{email}' already exists.") { }
}