namespace Streetcode.BLL.DTO.Users
{
    public class TokenResultDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
