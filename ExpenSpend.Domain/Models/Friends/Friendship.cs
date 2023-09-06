using ExpenSpend.Domain.Models.Users;

namespace ExpenSpend.Domain.Models.Friends;

public class Friendship : BaseEntity
{
    public Guid InitiatorId { get; set; }
    public User Initiator { get; set; }
    public Guid RecipientId { get; set; }
    public User Recipient { get; set; }
    public FriendshipStatus Status { get; set; } = FriendshipStatus.Pending;
}