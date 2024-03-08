using ExpenSpend.Domain.Models.Friends;
using ExpenSpend.Service.Models;

namespace ExpenSpend.Service.Contracts
{
    /// <summary>
    /// Provides functionality related to managing friendships.
    /// </summary>
    public interface IFriendAppService
    {
        /// <summary>
        /// Retrieves a list of all friends asynchronously.
        /// </summary>
        /// <returns>An API response.</returns>
        Task<Response> GetAllFriendsAsync();

        /// <summary>
        /// Retrieves a list of friendship requests asynchronously.
        /// </summary>
        /// <returns>An API response.</returns>
        Task<Response> GetFriendshipRequestsAsync();

        /// <summary>
        /// Retrieves a list of accepted friendships asynchronously.
        /// </summary>
        /// <returns>An API response.</returns>
        Task<Response> GetFriendshipsAsync();

        /// <summary>
        /// Retrieves a friend by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the friend to retrieve.</param>
        /// <returns>An API response.</returns>
        Task<Response> GetFriendByIdAsync(Guid id);

        /// <summary>
        /// Creates a new friendship asynchronously.
        /// </summary>
        /// <param name="recipientId">The unique identifier of the recipient user.</param>
        /// <returns>An API response.</returns>
        Task<Response> CreateFriendAsync(Guid recipientId);

        /// <summary>
        /// Updates a friendship asynchronously.
        /// </summary>
        /// <param name="friendship">The updated friendship data.</param>
        /// <returns>An API response.</returns>
        Task<Response> UpdateFriendAsync(Friendship friend);

        /// <summary>
        /// Soft deletes a friendship asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the friendship to delete.</param>
        /// <returns>An API response.</returns>
        Task<Response> SoftDeleteFriendAsync(Guid Id);

        /// <summary>
        /// Deletes a friendship asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the friendship to delete.</param>
        /// <returns>An API response.</returns>
        Task<Response> DeleteFriendAsync(Guid id);

        /// <summary>
        /// Accepts a friendship request asynchronously.
        /// </summary>
        /// <param name="friendshipId">The unique identifier of the friendship request to accept.</param>
        /// <returns>An API response.</returns>
        Task<Response> AcceptAsync(Guid friendshipId);

        /// <summary>
        /// Declines a friendship request asynchronously.
        /// </summary>
        /// <param name="friendshipId">The unique identifier of the friendship request to decline.</param>
        /// <returns>An API response.</returns>
        Task<Response> DeclineAsync(Guid friendshipId);

        /// <summary>
        /// Blocks a user asynchronously, ending the friendship.
        /// </summary>
        /// <param name="friendshipId">The unique identifier of the friendship to block.</param>
        /// <returns>An API response.</returns>
        Task<Response> BlockAsync(Guid friendshipId);

        /// <summary>
        /// Unblocks a previously blocked user asynchronously, restoring the friendship.
        /// </summary>
        /// <param name="friendshipId">The unique identifier of the friendship to unblock.</param>
        /// <returns>An API response.</returns>
        Task<Response> UnBlockAsync(Guid friendshipId);
    }
}
