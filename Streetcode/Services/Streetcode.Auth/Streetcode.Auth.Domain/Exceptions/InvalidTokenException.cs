namespace Streetcode.Auth.Domain.Exceptions;

/// <summary>
/// Exception thrown when a token is invalid, expired, or revoked.
/// </summary>
public class InvalidTokenException : DomainException
{
    public InvalidTokenException()
        : base("The provided token is invalid.") { }

    public InvalidTokenException(string message)
        : base(message) { }
}