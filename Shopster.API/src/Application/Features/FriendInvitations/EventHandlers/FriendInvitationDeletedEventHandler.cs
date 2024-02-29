using MediatR;
using Microsoft.Extensions.Logging;
using Shopster.API.Domain.Features.FriendInvitations.Events;

namespace Shopster.API.Application.Features.FriendInvitations.EventHandlers;

public sealed class FriendInvitationDeletedEventHandler 
    : INotificationHandler<FriendInvitationDeletedDomainEvent>
{
    private readonly ILogger<FriendInvitationDeletedEventHandler> _logger;

    public FriendInvitationDeletedEventHandler(ILogger<FriendInvitationDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(FriendInvitationDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Shopster Friend Invitation Deleted: {InvitationId}", notification.Invitation.Id);

        return Task.CompletedTask;
    }
}
