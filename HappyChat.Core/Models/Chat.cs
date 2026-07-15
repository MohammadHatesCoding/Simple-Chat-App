using HappyChat.Shared.Enums;

namespace HappyChat.Core.Models;

public class Chat : BaseEntity
{
    public ChatType Type { get; set; }
    public ChatPrivacy Privacy { get; set; }

    #region NavigationProperties
    public List<UserChat> Users { get; set; }
    public List<Message> Messages { get; set; }
    #endregion
}
