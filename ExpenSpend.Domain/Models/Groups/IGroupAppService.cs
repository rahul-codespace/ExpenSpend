﻿using ExpenSpend.Core.DTOs.Groups;
using ExpenSpend.Core.Helpers;
using ExpenSpend.Domain.Models.GroupMembers;

namespace ExpenSpend.Domain.Models.Groups
{
    /// <summary>
    /// Provides functionality related to user groups.
    /// </summary>
    public interface IGroupAppService
    {
        /// <summary>
        /// Retrieves a list of all groups asynchronously.
        /// </summary>
        /// <returns>A list of group data transfer objects.</returns>
        Task<List<GetGroupDto>> GetAllGroupsAsync();

        /// <summary>
        /// Retrieves a group by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the group to retrieve.</param>
        /// <returns>The group data transfer object.</returns>
        Task<GetGroupDto?> GetGroupByIdAsync(Guid id);

        /// <summary>
        /// Creates a new group asynchronously.
        /// </summary>
        /// <param name="input">The data for creating the group.</param>
        /// <returns>An API response indicating the result of the creation operation.</returns>
        Task<ApiResponse<GetGroupDto>> CreateGroupAsync(CreateGroupDto input);

        /// <summary>
        /// Creates a new group with members asynchronously.
        /// </summary>
        /// <param name="input">The data for creating the group with members.</param>
        /// <returns>An API response indicating the result of the creation operation.</returns>
        Task<ApiResponse<GetGroupDto>> CreateGroupWithMembers(CreateGroupWithMembersDto input);

        /// <summary>
        /// Updates a group asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the group to update.</param>
        /// <param name="group">The updated group data.</param>
        /// <returns>An API response indicating the result of the update operation.</returns>
        Task<ApiResponse<GetGroupDto>> UpdateGroupAsync(Guid id, UpdateGroupDto group);

        /// <summary>
        /// Deletes a group asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the group to delete.</param>
        /// <returns>An API response indicating the result of the deletion operation.</returns>
        Task<ApiResponse<bool>> DeleteGroupAsync(Guid id);

        /// <summary>
        /// Soft deletes a group asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the group to delete.</param>
        /// <returns>An API response indicating the result of the soft deletion operation.</returns>
        Task<ApiResponse<GetGroupDto>> SoftDeleteAsync(Guid id);

        /// <summary>
        /// Retrieves groups associated with a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>An API response containing a list of group data transfer objects.</returns>
        Task<ApiResponse<List<GetGroupDto>>> GetGroupsByUserId(Guid userId);
    }
}
