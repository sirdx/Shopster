using Microsoft.EntityFrameworkCore;
using Shopster.API.Domain.Common;

namespace Shopster.API.Domain.Features.FriendInvitations;

[Index(nameof(SenderUserId), nameof(RecipientUserId), IsUnique = true)]
public sealed class FriendInvitation : BaseEntity
{
    public Guid SenderUserId { get; set; }
    public Guid RecipientUserId { get; init; }
    public DateTimeOffset CreatedAt { get; set; }
}
