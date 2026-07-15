namespace HappyChat.Core.Models;

public class BaseEntity
{
    public int Id { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    public DateTime? UpdateDate { get; set; }

    public string CreateUser { get; set; } = "System";
    
    public string UpdateUser { get; set; } = string.Empty;

    public bool IsDeleted { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public bool IsBlocked { get; set; } = false;
}
