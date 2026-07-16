using HappyChat.Core.Models;
using HappyChat.Infrastructure.Persistance.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyChat.Infrastructure.Persistance.Configurations;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
    }
}
