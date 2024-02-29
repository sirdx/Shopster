using Shopster.API.Domain.Features.FriendInvitations;

namespace Shopster.API.Application.Features.FriendInvitations.Queries;

public sealed class FriendInvitationDto
{
    public Guid Id { get; init; }
    public Guid SenderUserId { get; init; }
    public Guid RecipientUserId { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}

public static class FriendInvitationExtensions
{
    public static FriendInvitationDto ToDto(this FriendInvitation invitation) => new()
    {
        Id = invitation.Id,
        SenderUserId = invitation.SenderUserId,
        RecipientUserId = invitation.RecipientUserId,
        CreatedAt = invitation.CreatedAt
    };
}
