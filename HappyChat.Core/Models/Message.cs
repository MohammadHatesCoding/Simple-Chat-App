using HappyChat.Shared.Enums;

namespace HappyChat.Core.Models;

public class Message : BaseEntity
{
    public int ChatId { get; set; }
    public int SenderId { get; set; }
    public int? RepliedTo { get; set; }
    public string Content { get; set; }
    public MessageStatus Status { get; set; } = MessageStatus.Sent;

    #region NavigationProperties

    public Chat Chat { get; set; }
    public User Sender { get; set; }
    public Message RepliedMessage { get; set; }
    #endregion
}