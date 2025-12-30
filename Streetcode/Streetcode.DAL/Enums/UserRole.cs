namespace Streetcode.DAL.Enums
{
    [Flags]
    public enum UserRole
    {
        None = 0,
        MainAdministrator = 1,
        Administrator = 2,
        Moderator = 3
    }
}
