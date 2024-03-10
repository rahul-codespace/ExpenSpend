using ExpenSpend.Domain.DTOs.GroupMembers;
using ExpenSpend.Domain.Helpers;

namespace ExpenSpend.Domain.Models.GroupMembers;

/// <summary>
/// Provides functionality related to group members.
/// </summary>
public interface IGroupMemberAppService
{
    /// <summary>
    /// Retrieves a list of all group members asynchronously.
    /// </summary>
    /// <returns>An API response containing a list of group member data transfer objects.</returns>
    Task<ApiResponse<List<GetGroupMemberDto>>> GetAllGroupMembersAsync();

    /// <summary>
    /// Retrieves a group member by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the group member to retrieve.</param>
    /// <returns>An API response containing the group member data transfer object.</returns>
    Task<ApiResponse<GetGroupMemberDto>> GetGroupMemberByIdAsync(Guid id);

    /// <summary>
    /// Creates a new group member asynchronously.
    /// </summary>
    /// <param name="input">The data for creating the group member.</param>
    /// <returns>An API response indicating the result of the creation operation.</returns>
    Task<ApiResponse<GetGroupMemberDto>> CreateGroupMemberAsync(CreateGroupMemberDto groupMember);

    /// <summary>
    /// Creates multiple group members asynchronously.
    /// </summary>
    /// <param name="input">The data for creating the group members.</param>
    /// <returns>An API response indicating the result of the creation operation.</returns>
    Task<ApiResponse<List<GetGroupMemberDto>>> CreateGroupMembersAsync(List<CreateGroupMemberDto> groupMembers);

    /// <summary>
    /// Soft deletes a group member asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the group member to delete.</param>
    /// <returns>An API response indicating the result of the soft deletion operation.</returns>
    Task<ApiResponse<GetGroupMemberDto>> SoftDeleteGroupMemberAsync(Guid id);

    /// <summary>
    /// Deletes a group member asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the group member to delete.</param>
    /// <returns>An API response indicating the result of the deletion operation.</returns>
    Task<ApiResponse<bool>> DeleteGroupMemberAsync(Guid id);

    /// <summary>
    /// Makes a group member an admin asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the group member to promote as an admin.</param>
    /// <returns>An API response indicating the result of promoting the group member as an admin.</returns>
    Task<ApiResponse<GetGroupMemberDto>> MakeGroupAdminAsync(Guid id);

    /// <summary>
    /// Removes a group member's admin role asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the group member to remove as an admin.</param>
    /// <returns>An API response indicating the result of removing the group member's admin role.</returns>
    Task<ApiResponse<GetGroupMemberDto>> RemoveGroupAdminAsync(Guid id);
}
