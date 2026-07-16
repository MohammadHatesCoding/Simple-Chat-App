using HappyChat.Core.Models;
using HappyChat.Infrastructure.Persistance.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyChat.Infrastructure.Persistance.Configurations;

public class MessageConfiguration : BaseEntityConfiguration<Message>
{
    public override void Configure(EntityTypeBuilder<Message> builder)
    {
        base.Configure(builder);

        // Relationships
        builder.HasOne(x => x.Chat)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Sender)
            .WithMany()
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        // Self-referencing: reply-to
        builder.HasOne(x => x.RepliedMessage)
            .WithMany()
            .HasForeignKey(x => x.RepliedTo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
