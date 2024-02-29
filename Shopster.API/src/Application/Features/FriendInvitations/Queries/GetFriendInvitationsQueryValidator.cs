using FluentValidation;

namespace Shopster.API.Application.Features.FriendInvitations.Queries;

public sealed class GetFriendInvitationsQueryValidator : AbstractValidator<GetFriendInvitationsQuery>
{
    public GetFriendInvitationsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
