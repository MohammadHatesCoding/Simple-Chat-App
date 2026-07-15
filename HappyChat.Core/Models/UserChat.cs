namespace HappyChat.Core.Models;

public class UserChat : BaseEntity
{
    public int UserId { get; set; }
    public int ChatId { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    #region NavigationProperties
    public User User { get; set; }
    public Chat Chat { get; set; }
    #endregion
}
