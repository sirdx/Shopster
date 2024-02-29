using Shopster.API.Domain.Common;

namespace Shopster.API.Domain.Features.FriendInvitations.Events;

public sealed record FriendInvitationCreatedDomainEvent(FriendInvitation Invitation) : IDomainEvent;
