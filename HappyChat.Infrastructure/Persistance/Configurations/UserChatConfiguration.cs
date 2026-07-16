using HappyChat.Core.Models;
using HappyChat.Infrastructure.Persistance.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyChat.Infrastructure.Persistance.Configurations;

public class UserChatConfiguration : BaseEntityConfiguration<UserChat>
{
    public override void Configure(EntityTypeBuilder<UserChat> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => new { x.UserId, x.ChatId }).IsUnique();

        // Relationships
        builder.HasOne(x => x.User)
            .WithMany(x => x.Chats)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Chat)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.ChatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
