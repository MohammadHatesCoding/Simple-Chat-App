namespace HappyChat.Core.Models;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string PhoneNumber { get; set; }

    public DateTime? BirthDate { get; set; }

    public bool IsOnline { get; set; }

    public bool IsLastSeenVisible { get; set; } = true;

    public DateTime? LastSeen { get; set; }

    public string OtpHash { get; set; } = string.Empty;
    
    public DateTime? OTPDuration { get; set; }


    #region NavigationProperties
    public List<Contact>? Contacts { get; set; }
    public List<UserChat>? Chats { get; set; }
    public List<UserRole>? UserRoles { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }
    #endregion
}