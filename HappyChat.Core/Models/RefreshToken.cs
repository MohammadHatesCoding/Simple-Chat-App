namespace HappyChat.Core.Models;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(1);
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int UserId { get; set; }
    public bool IsRevoked { get; set; } = false;
    public bool IsDeleted { get; set; } = false;

    #region Navigation Properties
    public User User { get; set; }
    #endregion
}