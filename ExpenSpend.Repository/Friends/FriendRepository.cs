using ExpenSpend.Domain.Context;
using ExpenSpend.Domain.Models.Friends;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Repository.Friends;

public class FriendRepository : IFriendRepository
{
    private readonly ExpenSpendDbContext _context;

    public FriendRepository(
        ExpenSpendDbContext context
        )
    {
        _context = context;
    }

    public async Task<Friendship> AddAsync(Guid InitiatorId, Guid RecipientId)
    {
        var existingFriendship = await _context.Friendships
            .FirstOrDefaultAsync(f =>
            (f.InitiatorId == InitiatorId && f.RecipientId == RecipientId) ||
            (f.InitiatorId == RecipientId && f.RecipientId == InitiatorId));
        if (existingFriendship == null)
        {
            var friendship = new Friendship
            {
                InitiatorId = InitiatorId,
                RecipientId = RecipientId,
                Status = FriendshipStatus.Pending
            };
            await _context.Friendships.AddAsync(friendship);
            await _context.SaveChangesAsync();
            return friendship;
        }
        else
        {
            return null;
        }
    }

    public async Task<Friendship> AcceptAsync(Guid friendshipId)
    {
        var friendship = await _context.Friendships.FirstOrDefaultAsync(f => f.Id == friendshipId);
        if (friendship != null)
        {
            friendship.Status = FriendshipStatus.Accepted;
            _context.Friendships.Update(friendship);
            await _context.SaveChangesAsync();
            return friendship;
        }
        else
        {
            return null;
        }
    }

    public async Task<Friendship> DeclineAsync(Guid friendshipId)
    {
        var friendship = await _context.Friendships.FirstOrDefaultAsync(f => f.Id == friendshipId);
        if (friendship != null)
        {
            friendship.Status = FriendshipStatus.Declined;
            _context.Friendships.Update(friendship);
            await _context.SaveChangesAsync();
            return friendship;
        }
        else
        {
            return null;
        }
    }

    public async Task<Friendship> BlockAsync(Guid friendshipId)
    {
        var friendship = await _context.Friendships.FirstOrDefaultAsync(f => f.Id == friendshipId);
        if (friendship != null)
        {
            friendship.Status = FriendshipStatus.Blocked;
            _context.Friendships.Update(friendship);
            await _context.SaveChangesAsync();
            return friendship;
        }
        else
        {
            return null;
        }
    }

    public async Task<Friendship> DeleteAsync(Guid friendshipId)
    {
        var friendship = await _context.Friendships.FirstOrDefaultAsync(f => f.Id == friendshipId);
        if (friendship != null)
        {
            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();
            return friendship;
        }
        else
        {
            return null;
        }
    }

    public async Task<List<Friendship>> GetFriendshipsAsync(Guid userId)
    {
        var friendships = await _context.Friendships
            .Where(f => (f.InitiatorId == userId || f.RecipientId == userId) && f.Status == FriendshipStatus.Accepted)
            .Include(f => f.Initiator)
            .Include(f => f.Recipient)
            .ToListAsync();
        return friendships;
    }

    public async Task<List<Friendship>> GetFriendshipRequestsAsync(Guid userId)
    {
        var friendships = await _context.Friendships
            .Where(f => f.RecipientId == userId && f.Status == FriendshipStatus.Pending)
            .Include(f => f.Initiator)
            .Include(f => f.Recipient)
            .ToListAsync();
        return friendships;
    }
}
