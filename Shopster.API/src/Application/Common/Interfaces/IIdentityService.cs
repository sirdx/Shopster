using Shopster.API.Application.Common.Models;

namespace Shopster.API.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(Guid userId);

    Task<bool> UserExistsAsync(Guid userId);

    Task<(Result Result, Guid UserId)> CreateUserAsync(string userName, string email, string password);

    Task<Result> DeleteUserAsync(Guid userId);
}
