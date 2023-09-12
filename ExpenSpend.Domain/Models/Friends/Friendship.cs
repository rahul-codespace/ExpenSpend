using ExpenSpend.Domain.Models.Users;

namespace ExpenSpend.Domain.Models.Friends;

public class Friendship : BaseEntity
{
    public string InitiatorId { get; set; }
    public User Initiator { get; set; }

    public string RecipientId { get; set; }
    public User Recipient { get; set; }
    public FriendshipStatus Status { get; set; } = FriendshipStatus.Pending;
}