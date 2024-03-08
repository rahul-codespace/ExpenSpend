using System.ComponentModel.DataAnnotations;

namespace ExpenSpend.Core.DTOs.Friends;

public class CreateFriendshipDto
{
    [Required]
    public Guid RecipientId { get; set; }
}
