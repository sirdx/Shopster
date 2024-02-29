using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopster.API.Application.Common.Interfaces;
using Shopster.API.Application.Common.Models;

namespace Shopster.API.Infrastructure.Identity;

public sealed class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string?> GetUserNameAsync(Guid userId)
    {
        ApplicationUser? user = await GetUserAsync(userId);
        return user?.UserName;
    }

    public async Task<bool> UserExistsAsync(Guid userId)
    {
        ApplicationUser? user = await GetUserAsync(userId);
        return user is not null;
    }

    public async Task<(Result Result, Guid UserId)> CreateUserAsync(string userName, string email, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = email
        };

        IdentityResult result = await _userManager.CreateAsync(user, password);
        return (result.ToApplicationResult(), Guid.Parse(user.Id));
    }

    public async Task<Result> DeleteUserAsync(Guid userId)
    {
        ApplicationUser? user = await GetUserAsync(userId);
        return user is not null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        IdentityResult result = await _userManager.DeleteAsync(user);
        return result.ToApplicationResult();
    }

    private async Task<ApplicationUser?> GetUserAsync(Guid userId)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId.ToString());
    }
}
