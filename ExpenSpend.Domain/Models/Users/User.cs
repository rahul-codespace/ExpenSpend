using ExpenSpend.Domain.Models.Friends;
using Microsoft.AspNetCore.Identity;

namespace ExpenSpend.Domain.Models.Users;

public class User : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }

    // Navigation properties
    public ICollection<Friendship> FriendshipsInitiated { get; set; } = new List<Friendship>();
    public ICollection<Friendship> FriendshipsReceived { get; set; } = new List<Friendship>();
}