using Shopster.API.Domain.Common;

namespace Shopster.API.Domain.Features.FriendInvitations.Events;

public sealed record FriendInvitationDeletedDomainEvent(FriendInvitation Invitation) : IDomainEvent;
