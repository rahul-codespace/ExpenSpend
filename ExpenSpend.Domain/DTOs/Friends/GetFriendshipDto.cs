using ExpenSpend.Domain.DTOs.Friends.Enums;
using ExpenSpend.Domain.DTOs.Users;

namespace ExpenSpend.Domain.DTOs.Friends;

public class GetFriendshipDto
{
    public Guid Id { get; set; }
    public Guid InitiatorId { get; set; }
    public Guid RecipientId { get; set; }
    public GetUserDto? Recipient { get; set; }
    public FriendshipStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Guid? CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}
