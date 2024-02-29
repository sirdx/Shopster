using Shopster.API.Infrastructure.Identity;
using Shopster.API.WebApi.Infrastructure;

namespace Shopster.API.WebApi.Endpoints;

public sealed class Account : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapIdentityApi<ApplicationUser>();
    }
}
