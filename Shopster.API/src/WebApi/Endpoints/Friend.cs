using MediatR;
using Shopster.API.Application.Common.Models;
using Shopster.API.Application.Features.FriendInvitations.Commands;
using Shopster.API.Application.Features.FriendInvitations.Queries;
using Shopster.API.WebApi.Infrastructure;

namespace Shopster.API.WebApi.Endpoints;

public sealed class Friend : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetFriendInvitations, "invites")
            .MapPost(CreateFriendInvitation, "invite")
            .MapDelete(DeleteFriendInvitation, "invite/{id}");
    }

    public Task<PaginatedList<FriendInvitationDto>> GetFriendInvitations(ISender sender, [AsParameters] GetFriendInvitationsQuery query)
    {
        return sender.Send(query);
    }

    public Task<Guid> CreateFriendInvitation(ISender sender, CreateFriendInvitationCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> DeleteFriendInvitation(ISender sender, Guid id)
    {
        await sender.Send(new DeleteFriendInvitationCommand(id));
        return Results.NoContent();
    }
}
