namespace Streetcode.Auth.Domain.Exceptions;

/// <summary>
/// Exception thrown when login credentials are invalid.
/// </summary>
public class InvalidCredentialsException : DomainException
{
    public InvalidCredentialsException()
        : base("Invalid email or password.") { }

    public InvalidCredentialsException(string message)
        : base(message) { }
}