using Microsoft.EntityFrameworkCore;
using Shopster.API.Domain.Features.FriendInvitations;

namespace Shopster.API.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<FriendInvitation> FriendInvitations { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
