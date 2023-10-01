using AutoMapper;
using ExpenSpend.Core.DTOs.GroupMembers;
using ExpenSpend.Core.Helpers;
using ExpenSpend.Data.Context;
using ExpenSpend.Domain;
using ExpenSpend.Domain.Models.GroupMembers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Service
{
    public class GroupMemberAppService : IGroupMemberAppService
    {
        private readonly IExpenSpendRepository<GroupMember> _groupMemberRepository;
        private readonly ExpenSpendDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public GroupMemberAppService(IExpenSpendRepository<GroupMember> groupMemberRepository, ExpenSpendDbContext context, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _groupMemberRepository = groupMemberRepository;
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<ApiResponse<List<GetGroupMemberDto>>> GetAllGroupMembersAsync()
        {
            var groupMembers = await _groupMemberRepository.GetAllAsync();
            if (groupMembers == null)
            {
                return new ApiResponse<List<GetGroupMemberDto>>
                {
                    Message = "Bad Request",
                    StatusCode = 400
                };
            }
            return new ApiResponse<List<GetGroupMemberDto>>
            {
                Data = _mapper.Map<List<GetGroupMemberDto>>(groupMembers),
                Message = "Group members found successfully",
                StatusCode = 200
            };
        }
        public async Task<ApiResponse<GetGroupMemberDto>> GetGroupMemberByIdAsync(Guid id)
        {
            var groupMember = await _groupMemberRepository.GetByIdAsync(id);
            if (groupMember == null)
            {
                return new ApiResponse<GetGroupMemberDto>
                {
                    Message = "Group member not found",
                    StatusCode = 404
                };
            }
            return new ApiResponse<GetGroupMemberDto>
            {
                Data = _mapper.Map<GetGroupMemberDto>(groupMember),
                Message = "Group member found successfully",
                StatusCode = 200
            };
        }
        public async Task<ApiResponse<GetGroupMemberDto>> CreateGroupMemberAsync(CreateGroupMemberDto input)
        {
            var checkIfGroupMemberExists = await _context.GroupMembers.FirstOrDefaultAsync(x => x.GroupId == input.GroupId && x.UserId == input.UserId);
            if (checkIfGroupMemberExists != null)
            {
                return new ApiResponse<GetGroupMemberDto>
                {
                    Message = "Group member already exists",
                    StatusCode = 400
                };
            }
            var currentUser = _httpContext.HttpContext?.User?.Identity?.Name;
            var currUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == currentUser);

            var groupMember = _mapper.Map<GroupMember>(input);
            groupMember.CreatedAt = DateTime.Now;
            groupMember.CreatedBy = input.UserId;
            var createdGroupMember = await _groupMemberRepository.CreateAsync(_mapper.Map<GroupMember>(groupMember));

            if (createdGroupMember == null)
            {
                return new ApiResponse<GetGroupMemberDto>
                {
                    Message = "Bad Request",
                    StatusCode = 400
                };
            }
            return new ApiResponse<GetGroupMemberDto>
            {
                Data = _mapper.Map<GetGroupMemberDto>(createdGroupMember),
                Message = "Group member created successfully",
                StatusCode = 200
            };
        }
        public async Task<ApiResponse<List<GetGroupMemberDto>>> CreateGroupMembersAsync(List<CreateGroupMemberDto> input)
        {
            var currentUser = _httpContext.HttpContext?.User?.Identity?.Name;
            var currUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == currentUser);

            var groupMembers = _mapper.Map<List<GroupMember>>(input);

            foreach (var member in groupMembers)
            {
                member.CreatedAt = DateTime.Now;
                member.CreatedBy = currUser.Id;
            }

            await _context.GroupMembers.AddRangeAsync(groupMembers);
            var createdGroupMembers = await _context.SaveChangesAsync();
            if (createdGroupMembers == 0)
            {
                return new ApiResponse<List<GetGroupMemberDto>>
                {
                    Message = "Bad Request",
                    StatusCode = 400
                };
            }
            return new ApiResponse<List<GetGroupMemberDto>>
            {
                Data = _mapper.Map<List<GetGroupMemberDto>>(groupMembers),
                Message = "Group members created successfully",
                StatusCode = 200
            };
        }

        public async Task<ApiResponse<GetGroupMemberDto>> SoftDeleteGroupMemberAsync(Guid id)
        {
            var groupMember = await _groupMemberRepository.GetByIdAsync(id);
            groupMember.IsDeleted = true;
            var deletedGroupMember = await _groupMemberRepository.UpdateAsync(groupMember);
            if (deletedGroupMember == null)
            {
                return new ApiResponse<GetGroupMemberDto>
                {
                    Message = "Bad Request",
                    StatusCode = 400
                };
            }
            return new ApiResponse<GetGroupMemberDto>
            {
                Data = _mapper.Map<GetGroupMemberDto>(deletedGroupMember),
                Message = "Group member deleted successfully",
                StatusCode = 200
            };
        }
        public async Task<ApiResponse<bool>> DeleteGroupMemberAsync(Guid id)
        {
            var result = await _groupMemberRepository.DeleteAsync(id);
            if (!result)
            {
                return new ApiResponse<bool>
                {
                    Data = false,
                    Message = "Bad Request or Group memeber not found!",
                    StatusCode = 400
                };
            }
            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Group member deleted successfully",
                StatusCode = 200
            };
        }
        public async Task<ApiResponse<GetGroupMemberDto>> MakeGroupAdminAsync(Guid id)
        {
            var groupMember = await _groupMemberRepository.GetByIdAsync(id);
            if (groupMember == null)
            {
                return new ApiResponse<GetGroupMemberDto>
                {
                    Message = "Group member not found",
                    StatusCode = 404
                };
            }
            groupMember.IsAdmin = true;
            var updatedGroupMember = await _groupMemberRepository.UpdateAsync(groupMember);
            if (updatedGroupMember == null)
            {
                return new ApiResponse<GetGroupMemberDto>
                {
                    Message = "Bad Request",
                    StatusCode = 400
                };
            }
            return new ApiResponse<GetGroupMemberDto>
            {
                Data = _mapper.Map<GetGroupMemberDto>(updatedGroupMember),
                Message = "Group member permoted as admin successfully",
                StatusCode = 200
            };
        }

        public async Task<ApiResponse<GetGroupMemberDto>> RemoveGroupAdminAsync(Guid id)
        {
            var groupMember = await _groupMemberRepository.GetByIdAsync(id);
            if (groupMember == null)
            {
                return new ApiResponse<GetGroupMemberDto>
                {
                    Message = "Group member not found",
                    StatusCode = 404
                };
            }
            groupMember.IsAdmin = false;
            var updatedGroupMember = await _groupMemberRepository.UpdateAsync(groupMember);
            if (updatedGroupMember == null)
            {
                return new ApiResponse<GetGroupMemberDto>
                {
                    Message = "Bad Request",
                    StatusCode = 400
                };
            }
            return new ApiResponse<GetGroupMemberDto>
            {
                Data = _mapper.Map<GetGroupMemberDto>(updatedGroupMember),
                Message = "Group member removed as admin successfully",
                StatusCode = 200
            };
        }
    }
}
