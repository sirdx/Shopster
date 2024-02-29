using System.Linq.Expressions;
using Ardalis.GuardClauses;
using MediatR;
using Shopster.API.Application.Common.Interfaces;
using Shopster.API.Application.Common.Models;
using Shopster.API.Domain.Features.FriendInvitations;

namespace Shopster.API.Application.Features.FriendInvitations.Queries;

public sealed record GetFriendInvitationsQuery(bool Received = true, int PageNumber = 1, int PageSize = 10) 
    : IRequest<PaginatedList<FriendInvitationDto>>;

public sealed class GetFriendInvitationsQueryHandler
    : IRequestHandler<GetFriendInvitationsQuery, PaginatedList<FriendInvitationDto>>
{
    private readonly IApplicationDbContext _appDbContext;
    private readonly ICurrentUserService _currentUserService;

    public GetFriendInvitationsQueryHandler(
        IApplicationDbContext appDbContext, 
        ICurrentUserService currentUserService)
    {
        _appDbContext = appDbContext;
        _currentUserService = currentUserService;
    }

    public async Task<PaginatedList<FriendInvitationDto>> Handle(GetFriendInvitationsQuery request, CancellationToken cancellationToken)
    {
        // TODO: Authentication Behaviour
        Guid? currentUserId = _currentUserService.Id;
        Guard.Against.Null(currentUserId);

        Expression<Func<FriendInvitation, bool>> where = x => x.RecipientUserId == currentUserId;

        if (!request.Received)
        {
            where = x => x.SenderUserId == currentUserId;
        }

        return await _appDbContext.FriendInvitations
            .Where(where)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => x.ToDto())
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
