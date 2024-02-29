using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopster.API.Domain.Features.FriendInvitations;

namespace Shopster.API.Infrastructure.Data.Configurations;

public class FriendInvitationConfiguration : IEntityTypeConfiguration<FriendInvitation>
{
    public void Configure(EntityTypeBuilder<FriendInvitation> builder)
    {
        builder.Property(t => t.RecipientUserId)
            .IsRequired();
    }
}
