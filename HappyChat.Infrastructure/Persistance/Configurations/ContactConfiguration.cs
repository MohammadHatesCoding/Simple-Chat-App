using HappyChat.Core.Models;
using HappyChat.Infrastructure.Persistance.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyChat.Infrastructure.Persistance.Configurations;

public class ContactConfiguration : BaseEntityConfiguration<Contact>
{
    public override void Configure(EntityTypeBuilder<Contact> builder)
    {
        base.Configure(builder);

        // relationships
        builder.HasOne(x => x.Owner)
            .WithMany(x => x.Contacts)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
