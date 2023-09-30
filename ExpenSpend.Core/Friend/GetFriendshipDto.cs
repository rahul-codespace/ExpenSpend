using ExpenSpend.Domain.Models;
using ExpenSpend.Domain.Models.Friends;
using ExpenSpend.Domain.Models.Users;

namespace ExpenSpend.Core.Friend;

public class GetFriendshipDto : BaseEntity
{
    public Guid InitiatorId { get; set; }
    public Guid RecipientId { get; set; }
    public ESUser Recipient { get; set; }
    public FriendshipStatus Status { get; set; }
}
