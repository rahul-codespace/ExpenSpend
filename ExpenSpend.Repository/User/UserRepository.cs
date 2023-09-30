using ExpenSpend.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Repository.User;

    
public class UserRepository : IUserRepository
{
    private readonly UserManager<ESUser> _userManager;

    public UserRepository(UserManager<ESUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<List<ESUser>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }
    
    public async Task<ESUser?> GetUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<IdentityResult> UpdateUserAsync(ESUser user)
    {
        
        return await _userManager.UpdateAsync(user);

    }
    
    public async Task<IdentityResult> DeleteUserAsync(ESUser user)
    {
        return await _userManager.DeleteAsync(user);
    }
}