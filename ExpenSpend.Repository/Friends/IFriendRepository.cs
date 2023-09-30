using ExpenSpend.Domain.Models.Friends;

namespace ExpenSpend.Repository.Friends
{
    public interface IFriendRepository
    {
        Task<Friendship> AcceptAsync(Guid friendshipId);
        Task<Friendship> AddAsync(Guid InitiatorId, Guid RecipientId);
        Task<Friendship> BlockAsync(Guid friendshipId);
        Task<Friendship> DeclineAsync(Guid friendshipId);
        Task<Friendship> DeleteAsync(Guid friendshipId);
        Task<List<Friendship>> GetFriendshipRequestsAsync(Guid userId);
        Task<List<Friendship>> GetFriendshipsAsync(Guid userId);
    }
}