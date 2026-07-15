namespace HappyChat.Core.Models;

public class Contact : BaseEntity
{
    public int OwnerId { get; set; }

    public int ContactUserId { get; set; }

    public string? ContactPhoneNumber { get; set; }

    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;


    #region NavigationProperties
    public User Owner { get; set; }
    public User ContactUser { get; set; }
    #endregion
}