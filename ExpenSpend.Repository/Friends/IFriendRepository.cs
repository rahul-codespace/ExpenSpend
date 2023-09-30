using ExpenSpend.Domain.Models.Friends;

namespace ExpenSpend.Repository.Friends
{
    /// <summary>
    /// Interface of friend repository.
    /// </summary>
    public interface IFriendRepository
    {
        /// <summary>
        /// Accepts the friend request when the user confirms.
        /// </summary>
        /// <param name="friendshipId">Friendship Id of the friend request.</param>
        /// <returns>Awaitable friendship object.</returns>
        Task<Friendship> AcceptAsync(Guid friendshipId);

        /// <summary>
        /// Sends a new friend request.
        /// </summary>
        /// <param name="InitiatorId">GUID of the friend request initiator.</param>
        /// <param name="RecipientId">GUID of the friend request recipient.</param>
        /// <returns>Awaitable friendship object.</returns>
        Task<Friendship> AddAsync(Guid InitiatorId, Guid RecipientId);

        /// <summary>
        /// Blocks a friend from user&apos;s friends list.
        /// </summary>
        /// <param name="friendshipId">Friendship Id of the friend to be blocked.</param>
        /// <returns>Awaitable friendship object.</returns>
        Task<Friendship> BlockAsync(Guid friendshipId);

        /// <summary>
        /// Declines the friend request when the user rejects it.
        /// </summary>
        /// <param name="friendshipId">Friendship Id of the friend request.</param>
        /// <returns>Awaitable friendship object.</returns>
        Task<Friendship> DeclineAsync(Guid friendshipId);

        /// <summary>
        /// Deletes a friend entry from user&apos;s friends list.
        /// </summary>
        /// <param name="friendshipId">Friendship Id of the friend to be deleted.</param>
        /// <returns>Awaitable friendship object.</returns>
        Task<Friendship> DeleteAsync(Guid friendshipId);

        /// <summary>
        /// Returns a list of friend requests to a user.
        /// </summary>
        /// <param name="userId">GUID of the user.</param>
        /// <returns>Awaitable list of friendship objects.</returns>
        Task<List<Friendship>> GetFriendshipRequestsAsync(Guid userId);

        /// <summary>
        /// Returns a list of friends for a user.
        /// </summary>
        /// <param name="userId">GUID of the user.</param>
        /// <returns>Awaitable list of friendship objects.</returns>
        Task<List<Friendship>> GetFriendshipsAsync(Guid userId);
    }
}