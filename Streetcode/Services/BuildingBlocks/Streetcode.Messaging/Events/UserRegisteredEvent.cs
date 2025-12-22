namespace Streetcode.Messaging.Events
{
    public record UserRegisteredEvent(
        int UserId,
        string Email,
        string Name,
        string Surname,
        DateTime RegisteredAtUtc);
}
