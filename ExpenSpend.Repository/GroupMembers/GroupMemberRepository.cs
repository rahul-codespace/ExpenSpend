using ExpenSpend.Domain.Context;
using ExpenSpend.Domain.Models.GroupMembers;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Repository.GroupMembers;

public class GroupMemberRepository : IGroupMemberRepository
{
    private readonly ExpenSpendDbContext _context;
    public GroupMemberRepository(ExpenSpendDbContext context)
    {
        _context = context;
    }
    public async Task<GroupMember> CreateGroupMember(GroupMember groupMember)
    {
        if(!await CheckExistingGroupMemberAsync(groupMember.GroupId, groupMember.UserId))
        {
            await _context.GroupMembers.AddAsync(groupMember);
            await _context.SaveChangesAsync();
            return groupMember;
        }
        return null;
    }
    public async Task<List<GroupMember>> CreateGroupMembers(List<GroupMember> groupMembers)
    {
        await _context.GroupMembers.AddRangeAsync(groupMembers);
        await _context.SaveChangesAsync();
        return groupMembers;
    }
    public async Task<GroupMember?> GetGroupMemberById(Guid id)
    {
        return await _context.GroupMembers.Include(g=>g.Group).FirstOrDefaultAsync(m => m.Id == id);
    }
    public async Task<GroupMember> UpdateGroupMember(GroupMember groupMember)
    {
        _context.GroupMembers.Update(groupMember);
        await _context.SaveChangesAsync();
        return groupMember;
    }
    public async Task<GroupMember> SoftDeleteGroupMember(GroupMember groupMember)
    {
        _context.GroupMembers.Update(groupMember);
        await _context.SaveChangesAsync();
        return groupMember;
    }
    public async Task DeleteGroupMember(GroupMember groupMember)
    {
        _context.GroupMembers.Remove(groupMember);
        await _context.SaveChangesAsync();
    }
    public async Task<List<GroupMember>> GetAllGroupMembers()
    {
        var result = await _context.GroupMembers.ToListAsync();
        return result;
    }
    public async Task<List<GroupMember>> GetGroupMembersByGroupId(Guid groupId)
    {
        var result = await _context.GroupMembers.Where(x => x.GroupId == groupId).ToListAsync();
        return result;
    }
    public async Task<List<GroupMember>> GetGroupMembersByUserId(Guid userId)
    {
        var result = await _context.GroupMembers.Where(x => x.UserId == userId).ToListAsync();
        return result;
    }

    public async Task<bool> CheckExistingGroupMemberAsync(Guid groupId, Guid userId)
    {
        return await _context.GroupMembers.AnyAsync(m => m.GroupId == groupId && m.UserId == userId);
    }
}
