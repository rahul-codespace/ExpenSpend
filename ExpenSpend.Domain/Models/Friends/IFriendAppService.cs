using ExpenSpend.Core.DTOs.Friends;
using ExpenSpend.Core.Helpers;

namespace ExpenSpend.Domain.Models.Friends
{
    /// <summary>
    /// Provides functionality related to managing friendships.
    /// </summary>
    public interface IFriendAppService
    {
        /// <summary>
        /// Retrieves a list of all friends asynchronously.
        /// </summary>
        /// <returns>An API response containing a list of friendship data transfer objects.</returns>
        Task<ApiResponse<List<GetFriendshipDto>>> GetAllFriendsAsync();

        /// <summary>
        /// Retrieves a list of friendship requests asynchronously.
        /// </summary>
        /// <returns>An API response containing a list of friendship data transfer objects representing pending requests.</returns>
        Task<ApiResponse<List<GetFriendshipDto>>> GetFriendshipRequestsAsync();

        /// <summary>
        /// Retrieves a list of accepted friendships asynchronously.
        /// </summary>
        /// <returns>An API response containing a list of friendship data transfer objects representing accepted friendships.</returns>
        Task<ApiResponse<List<GetFriendshipDto>>> GetFriendshipsAsync();

        /// <summary>
        /// Retrieves a friend by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the friend to retrieve.</param>
        /// <returns>An API response containing the friendship data transfer object.</returns>
        Task<ApiResponse<GetFriendshipDto>> GetFriendByIdAsync(Guid id);

        /// <summary>
        /// Creates a new friendship asynchronously.
        /// </summary>
        /// <param name="recipientId">The unique identifier of the recipient user.</param>
        /// <returns>An API response indicating the result of the creation operation.</returns>
        Task<ApiResponse<GetFriendshipDto>> CreateFriendAsync(Guid recipientId);

        /// <summary>
        /// Updates a friendship asynchronously.
        /// </summary>
        /// <param name="friendship">The updated friendship data.</param>
        /// <returns>An API response indicating the result of the update operation.</returns>
        Task<ApiResponse<GetFriendshipDto>> UpdateFriendAsync(Friendship friend);

        /// <summary>
        /// Soft deletes a friendship asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the friendship to delete.</param>
        /// <returns>An API response indicating the result of the soft deletion operation.</returns>
        Task<ApiResponse<GetFriendshipDto>> SoftDeleteFriendAsync(Guid Id);

        /// <summary>
        /// Deletes a friendship asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the friendship to delete.</param>
        /// <returns>An API response indicating the result of the deletion operation.</returns>
        Task<ApiResponse<bool>> DeleteFriendAsync(Guid id);

        /// <summary>
        /// Accepts a friendship request asynchronously.
        /// </summary>
        /// <param name="friendshipId">The unique identifier of the friendship request to accept.</param>
        /// <returns>An API response indicating the result of accepting the friendship request.</returns>

        Task<ApiResponse<GetFriendshipDto>> AcceptAsync(Guid friendshipId);

        /// <summary>
        /// Declines a friendship request asynchronously.
        /// </summary>
        /// <param name="friendshipId">The unique identifier of the friendship request to decline.</param>
        /// <returns>An API response indicating the result of declining the friendship request.</returns>
        Task<ApiResponse<GetFriendshipDto>> DeclineAsync(Guid friendshipId);

        /// <summary>
        /// Blocks a user asynchronously, ending the friendship.
        /// </summary>
        /// <param name="friendshipId">The unique identifier of the friendship to block.</param>
        /// <returns>An API response indicating the result of blocking the friendship.</returns>
        Task<ApiResponse<GetFriendshipDto>> BlockAsync(Guid friendshipId);

        /// <summary>
        /// Unblocks a previously blocked user asynchronously, restoring the friendship.
        /// </summary>
        /// <param name="friendshipId">The unique identifier of the friendship to unblock.</param
        Task<ApiResponse<GetFriendshipDto>> UnBlockAsync(Guid friendshipId);
    }
}
