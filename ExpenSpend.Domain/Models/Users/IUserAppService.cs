using ExpenSpend.Core.DTOs.Users;
using ExpenSpend.Core.Helpers;

namespace ExpenSpend.Domain.Models.Users
{
    public interface IUserAppService
    {
        Task<GetUserDto> GetLoggedInUser();
        Task<ApiResponse<GetUserDto>> DeleteUserAsync(Guid id);
        Task<List<GetUserDto>> GetAllUsersAsync();
        Task<ESUser> GetUserByEmailAsync(string email);
        Task<ESUser> GetUserByIdAsync(string id);
        Task<ESUser> GetUserByUserNameAsync(string userName);
        Task<ApiResponse<GetUserDto>> UpdateUserAsync(Guid id, UpdateUserDto user);
    }
}
