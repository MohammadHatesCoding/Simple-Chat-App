namespace HappyChat.Core.Models;

public class Role : BaseEntity
{
    public string Title { get; set; }

    #region Navigation Properties
    public List<UserRole> UserRoles { get; set; }
    #endregion
}