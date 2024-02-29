using Ardalis.GuardClauses;
using MediatR;
using Shopster.API.Application.Common.Interfaces;
using Shopster.API.Domain.Features.FriendInvitations;
using Shopster.API.Domain.Features.FriendInvitations.Events;

namespace Shopster.API.Application.Features.FriendInvitations.Commands;

public sealed record DeleteFriendInvitationCommand(Guid Id) : IRequest;

public sealed class DeleteFriendInvitationCommandHandler
    : IRequestHandler<DeleteFriendInvitationCommand>
{
    private readonly IApplicationDbContext _appDbContext;
    private readonly ICurrentUserService _currentUserService;

    public DeleteFriendInvitationCommandHandler(
        IApplicationDbContext appDbContext, 
        ICurrentUserService currentUserService)
    {
        _appDbContext = appDbContext;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteFriendInvitationCommand request, CancellationToken cancellationToken)
    {
        FriendInvitation? invitation = await _appDbContext.FriendInvitations
            .FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, invitation);

        // TODO: Authentication Behaviour
        Guid? currentUserId = _currentUserService.Id;
        Guard.Against.Null(currentUserId);

        if (currentUserId != invitation.SenderUserId)
        {
            throw new ArgumentException("You are not allowed to remove this invitation", nameof(request.Id));
        }

        _appDbContext.FriendInvitations.Remove(invitation);
        invitation.AddDomainEvent(new FriendInvitationDeletedDomainEvent(invitation));
        
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}
