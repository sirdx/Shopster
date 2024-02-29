using MediatR;
using Shopster.API.Application.Features.FriendInvitations.Commands;
using Shopster.API.WebApi.Infrastructure;

namespace Shopster.API.WebApi.Endpoints;

public sealed class Friend : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateFriendInvitation, "invite")
            .MapDelete(DeleteFriendInvitation, "invite/{id}");
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
