namespace HappyChat.Core.Models;

public class UserRole
{
    public int UserId { get; set; }
    public int RoleId { get; set; }

    #region Navigation Properties
    public User User { get; set; }
    public Role Role { get; set; }
    #endregion
}