using MediatR;
using Microsoft.Extensions.Logging;
using Shopster.API.Domain.Features.FriendInvitations.Events;

namespace Shopster.API.Application.Features.FriendInvitations.EventHandlers;

public sealed class FriendInvitationCreatedEventHandler 
    : INotificationHandler<FriendInvitationCreatedDomainEvent>
{
    private readonly ILogger<FriendInvitationCreatedEventHandler> _logger;

    public FriendInvitationCreatedEventHandler(ILogger<FriendInvitationCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(FriendInvitationCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Shopster Friend Invitation Created: {InvitationId}", notification.Invitation.Id);

        return Task.CompletedTask;
    }
}
