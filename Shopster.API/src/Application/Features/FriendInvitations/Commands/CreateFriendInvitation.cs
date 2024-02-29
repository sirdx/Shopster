using Ardalis.GuardClauses;
using MediatR;
using Shopster.API.Application.Common.Interfaces;
using Shopster.API.Domain.Features.FriendInvitations;
using Shopster.API.Domain.Features.FriendInvitations.Events;

namespace Shopster.API.Application.Features.FriendInvitations.Commands;

public sealed record CreateFriendInvitationCommand(Guid RecipientUserId) : IRequest<Guid>;

public sealed class CreateFriendInvitationCommandHandler 
    : IRequestHandler<CreateFriendInvitationCommand, Guid>
{
    private readonly IApplicationDbContext _appDbContext;
    private readonly TimeProvider _timeProvider;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public CreateFriendInvitationCommandHandler(
        IApplicationDbContext applicationDbContext, 
        TimeProvider timeProvider,
        ICurrentUserService currentUserService,
        IIdentityService identityService)
    {
        _appDbContext = applicationDbContext;
        _timeProvider = timeProvider;
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    public async Task<Guid> Handle(CreateFriendInvitationCommand request, CancellationToken cancellationToken)
    {
        bool recipientExists = await _identityService.UserExistsAsync(request.RecipientUserId);

        if (!recipientExists)
        {
            throw new NotFoundException(request.RecipientUserId.ToString(), nameof(request.RecipientUserId));
        }

        // TODO: Authentication Behaviour
        Guid? currentUserId = _currentUserService.Id;
        Guard.Against.Null(currentUserId);

        if (currentUserId == request.RecipientUserId)
        {
            throw new ArgumentException("Recipient can not be the same as the sender", nameof(request.RecipientUserId));
        }

        var invitation = new FriendInvitation
        {
            SenderUserId = (Guid)currentUserId,
            RecipientUserId = request.RecipientUserId,
            CreatedAt = _timeProvider.GetUtcNow()
        };

        _appDbContext.FriendInvitations.Add(invitation);

        invitation.AddDomainEvent(new FriendInvitationCreatedDomainEvent(invitation));
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return invitation.Id;
    }
}
