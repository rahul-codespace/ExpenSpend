using ExpenSpend.Domain.Context;
using ExpenSpend.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace ExpenSpend.Repository.Friend;

public class FriendRepository
{
    private readonly ExpenSpendDbContext _context;
    private readonly UserManager<ESUser> _userManager;
}
