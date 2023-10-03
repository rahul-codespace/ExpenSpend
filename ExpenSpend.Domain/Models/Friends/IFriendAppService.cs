using ExpenSpend.Core.DTOs.Friends;
using ExpenSpend.Core.Helpers;

namespace ExpenSpend.Domain.Models.Friends
{
    public interface IFriendAppService
    {
        Task<ApiResponse<List<GetFriendshipDto>>> GetAllFriendsAsync();
        Task<ApiResponse<List<GetFriendshipDto>>> GetFriendshipRequestsAsync();
        Task<ApiResponse<List<GetFriendshipDto>>> GetFriendshipsAsync();
        Task<ApiResponse<GetFriendshipDto>> GetFriendByIdAsync(Guid id);
        Task<ApiResponse<GetFriendshipDto>> CreateFriendAsync(Guid recipientId);
        Task<ApiResponse<GetFriendshipDto>> UpdateFriendAsync(Friendship friend);
        Task<ApiResponse<GetFriendshipDto>> SoftDeleteFriendAsync(Guid Id);
        Task<ApiResponse<bool>> DeleteFriendAsync(Guid id);
        Task<ApiResponse<GetFriendshipDto>> AcceptAsync(Guid friendshipId);
        Task<ApiResponse<GetFriendshipDto>> DeclineAsync(Guid friendshipId);
        Task<ApiResponse<GetFriendshipDto>> BlockAsync(Guid friendshipId);
        Task<ApiResponse<GetFriendshipDto>> UnBlockAsync(Guid friendshipId);
    }
}
