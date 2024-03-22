using System.ComponentModel.DataAnnotations;

namespace ExpenSpend.Domain.DTOs.Friends;

public class CreateFriendshipDto
{
    [Required]
    public Guid RecipientId { get; set; }
}
