using ExpenSpend.Core.DTOs.Groups;
using ExpenSpend.Core.Helpers;
using ExpenSpend.Domain.Models.GroupMembers;

namespace ExpenSpend.Domain.Models.Groups
{
    public interface IGroupAppService
    {
        Task<List<GetGroupDto>> GetAllGroupsAsync();
        Task<GetGroupDto> GetGroupByIdAsync(Guid id);
        Task<ApiResponse<GetGroupDto>> CreateGroupAsync(CreateGroupDto input);
        Task<ApiResponse<GetGroupDto>> CreateGroupWithMembers(CreateGroupWithMembersDto input);
        Task<ApiResponse<GetGroupDto>> UpdateGroupAsync(Guid id, UpdateGroupDto group);
        Task<ApiResponse<bool>> DeleteGroupAsync(Guid id);
        Task<ApiResponse<GetGroupDto>> SoftDeleteAsync(Guid id);
        Task<ApiResponse<List<GetGroupDto>>> GetGroupsByUserId(Guid userId);
    }
}
