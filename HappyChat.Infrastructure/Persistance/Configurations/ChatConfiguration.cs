using HappyChat.Core.Models;
using HappyChat.Infrastructure.Persistance.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyChat.Infrastructure.Persistance.Configurations;

public class ChatConfiguration : BaseEntityConfiguration<Chat>
{
    public override void Configure(EntityTypeBuilder<Chat> builder)
    {
        base.Configure(builder);
    }
}
