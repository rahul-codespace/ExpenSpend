using ExpenSpend.Domain.Models.Friends;
using Microsoft.AspNetCore.Identity;

namespace ExpenSpend.Domain.Models.Users;

public class ESUser : IdentityUser<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    // Navigation properties
    public ICollection<Friendship> FriendshipsInitiated { get; set; } = new List<Friendship>();
    public ICollection<Friendship> FriendshipsReceived { get; set; } = new List<Friendship>();
}