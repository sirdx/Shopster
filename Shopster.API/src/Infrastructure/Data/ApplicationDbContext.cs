using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopster.API.Application.Common.Interfaces;
using Shopster.API.Domain.Features.FriendInvitations;
using Shopster.API.Infrastructure.Identity;

namespace Shopster.API.Infrastructure.Data;

public sealed class ApplicationDbContext 
    : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public DbSet<FriendInvitation> FriendInvitations => Set<FriendInvitation>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
