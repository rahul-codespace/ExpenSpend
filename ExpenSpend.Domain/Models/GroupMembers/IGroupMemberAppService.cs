using ExpenSpend.Core.DTOs.GroupMembers;
using ExpenSpend.Core.Helpers;

namespace ExpenSpend.Domain.Models.GroupMembers;

public interface IGroupMemberAppService
{
    Task<ApiResponse<List<GetGroupMemberDto>>> GetAllGroupMembersAsync();
    Task<ApiResponse<GetGroupMemberDto>> GetGroupMemberByIdAsync(Guid id);
    Task<ApiResponse<GetGroupMemberDto>> CreateGroupMemberAsync(CreateGroupMemberDto groupMember);
    Task<ApiResponse<List<GetGroupMemberDto>>> CreateGroupMembersAsync(List<CreateGroupMemberDto> groupMembers);
    Task<ApiResponse<GetGroupMemberDto>> SoftDeleteGroupMemberAsync(Guid id);
    Task<ApiResponse<bool>> DeleteGroupMemberAsync(Guid id);
    Task<ApiResponse<GetGroupMemberDto>> MakeGroupAdminAsync(Guid id);
    Task<ApiResponse<GetGroupMemberDto>> RemoveGroupAdminAsync(Guid id);
}
