using AutoMapper;
using ExpenSpend.Core.DTOs.Groups;
using ExpenSpend.Core.Helpers;
using ExpenSpend.Data.Context;
using ExpenSpend.Domain;
using ExpenSpend.Domain.Models.GroupMembers;
using ExpenSpend.Domain.Models.Groups;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Service;

public class GroupAppService : IGroupAppService
{
    private readonly IExpenSpendRepository<Group> _groupRepository;
    private readonly ExpenSpendDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContext;

    public GroupAppService(IExpenSpendRepository<Group> groupRepository, ExpenSpendDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _groupRepository = groupRepository;
        _context = context;
        _mapper = mapper;
        _httpContext = httpContextAccessor;
    }

    public async Task<List<GetGroupDto>> GetAllGroupsAsync()
    {
        return _mapper.Map<List<GetGroupDto>>(await _groupRepository.GetAllAsync());
    }
    public async Task<GetGroupDto?> GetGroupByIdAsync(Guid id)
    {
        var result =  _mapper.Map<GetGroupDto>(await _groupRepository.GetByIdAsync(id));
        if (result != null)
        {
            return result;
        }

        return null;
    }
    public async Task<ApiResponse<GetGroupDto>> CreateGroupAsync(CreateGroupDto input)
    {
        var currentUser = _httpContext.HttpContext?.User?.Identity?.Name;
        var currUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == currentUser);
        var group = new Group
        {
            Name = input.Name,
            About = input.About,
            CreatedBy = currUser?.Id,
            CreatedAt = DateTime.Now,
        };
        var createdGroup = await _groupRepository.CreateAsync(group);
        if (createdGroup == null)
        {
            return new ApiResponse<GetGroupDto>
            {
                Message = "Bad Request",
                StatusCode = 400
            };
        }
        var groupMember = new GroupMember
        {
            GroupId = createdGroup.Id,
            UserId = currUser.Id,
            IsAdmin = true,
            CreatedAt = DateTime.Now,
            CreatedBy = currUser.Id
        };
        _context.GroupMembers.Add(groupMember);
        await _context.SaveChangesAsync();
        return new ApiResponse<GetGroupDto>
        {
            Data = _mapper.Map<GetGroupDto>(createdGroup),
            Message = "Group created successfully",
            StatusCode = 201
        };
    }
    public async Task<ApiResponse<GetGroupDto>> CreateGroupWithMembers(CreateGroupWithMembersDto input)
    {
        var currentUser = _httpContext.HttpContext?.User?.Identity?.Name;
        var currUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == currentUser);
        var group = new Group
        {
            Name = input.Name,
            About = input.About,
            CreatedBy = currUser?.Id,
            CreatedAt = DateTime.Now,
        };
        var createdGroup = await _groupRepository.CreateAsync(group);
        if (createdGroup == null)
        {
            return new ApiResponse<GetGroupDto>
            {
                Message = "Bad Request",
                StatusCode = 400
            };
        }
        var groupMembers = new List<GroupMember>();
        foreach (var memberId in input.MemberIds)
        {
            groupMembers.Add(new GroupMember
            {
                GroupId = createdGroup.Id,
                UserId = memberId
            });
        }
        _context.GroupMembers.AddRange(groupMembers);
        await _context.SaveChangesAsync();
        return new ApiResponse<GetGroupDto>
        {
            Data = _mapper.Map<GetGroupDto>(createdGroup),
            Message = "Group created successfully",
            StatusCode = 201
        };
    }
    public async Task<ApiResponse<GetGroupDto>> UpdateGroupAsync(Guid id, UpdateGroupDto group)
    {
        var existingGroup = await _groupRepository.GetByIdAsync(id);
        if (existingGroup == null)
        {
            return new ApiResponse<GetGroupDto>
            {
                Message = "Group not found",
                StatusCode = 404
            };
        }

        var currentUser = _httpContext.HttpContext?.User?.Identity?.Name;
        var currUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == currentUser);
        existingGroup.Name = group.Name;
        existingGroup.About = group.About;
        existingGroup.ModifiedAt = DateTime.Now;
        existingGroup.ModifiedBy = currUser.Id;

        var result = await _groupRepository.UpdateAsync(existingGroup);
        if (result == null)
        {
            return new ApiResponse<GetGroupDto>
            {
                Message = "Bad Request",
                StatusCode = 400
            };
        }
        return new ApiResponse<GetGroupDto>
        {
            Data = _mapper.Map<GetGroupDto>(result),
            Message = "Group updated successfully",
            StatusCode = 200
        };
    }
    public async Task<ApiResponse<GetGroupDto>> SoftDeleteAsync(Guid id)
    {
        var existingGroup = await _groupRepository.GetByIdAsync(id);
        if (existingGroup == null)
        {
            return new ApiResponse<GetGroupDto>
            {
                Message = "Group not found",
                StatusCode = 404
            };
        }
        existingGroup.IsDeleted= true;
        var result = await _groupRepository.UpdateAsync(existingGroup);
        if (result == null)
        {
            return new ApiResponse<GetGroupDto>
            {
                Message = "Bad Request",
                StatusCode = 400
            };
        }
        return new ApiResponse<GetGroupDto>
        {
            Data = _mapper.Map<GetGroupDto>(result),
            Message = "Group updated successfully",
            StatusCode = 200
        };
    }
    public async Task<ApiResponse<bool>> DeleteGroupAsync(Guid id)
    {
        var result = await _groupRepository.DeleteAsync(id);
        if (!result)
        {
            return new ApiResponse<bool>
            {
                Data = false,
                Message = "Bad Request or Group not found!",
                StatusCode = 400
            };
        }
        return new ApiResponse<bool>
        {
            Data = true,
            Message = "Group deleted successfully",
            StatusCode = 200
        };
    }


    public async Task<ApiResponse<List<GetGroupDto>>> GetGroupsByUserId(Guid userId)
    {
        var groups = await _context.Groups.Where(x => x.Members.Any(x => x.UserId == userId)).ToListAsync();
        if (groups == null)
        {
            return new ApiResponse<List<GetGroupDto>>
            {
                Message = "Groups not found",
                StatusCode = 404
            };
        }   
        return new ApiResponse<List<GetGroupDto>>
        {
            Data = _mapper.Map<List<GetGroupDto>>(groups),
            Message = "Groups found successfully",
            StatusCode = 200
        };
    }
}
