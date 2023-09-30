using ExpenSpend.Domain.Context;
using ExpenSpend.Domain.Models.Groups;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Repository.Groups
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ExpenSpendDbContext _context;

        public GroupRepository(ExpenSpendDbContext context)
        {
            _context = context;
        }

        public async Task<Group> CreateGroup(Group group)
        {
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<Group?> GetGroupById(Guid id)
        {
            return await _context.Groups.Include(x => x.Members).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Group> UpdateGroup(Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task DeleteGroup(Group group)
        {
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }
        public async Task<Group> SoftDeleteGroup(Group group)
        {
            group.IsDeleted = true;
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<List<Group>> GetAllGroups()
        {
            var result = await _context.Groups.ToListAsync();
            return result;
        }

        public async Task<List<Group>> GetGroupsByUserId(Guid userId)
        {
            var result = await _context.Groups.Where(x => x.Members.Any(x => x.UserId == userId)).ToListAsync();
            return result;
        }

        public async Task<List<Group>> GetGroupsByUserEmail(string email)
        {
            var result = await _context.Groups.Where(x => x.Members.Any(x => x.User.Email == email)).ToListAsync();
            return result;
        }
    }
}
