using ExpenSpend.Domain.Models.Groups;

namespace ExpenSpend.Repository.Groups
{
    /// <summary>
    /// The interface for creating, deleting, retrieving, and updating groups for ExpenSpend.
    /// </summary>
    public interface IGroupRepository
    {
        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <param name="group">The group to create.</param>
        /// <returns>The created group.</returns>
        Task<Group> CreateGroup(Group group);

        /// <summary>
        /// Deletes a group.
        /// </summary>
        /// <param name="group">The group to delete.</param>
        Task DeleteGroup(Group group);

        /// <summary>
        /// Soft Deletes a group.
        /// </summary>
        /// <param name="group">The group to delete.</param>
        Task<Group> SoftDeleteGroup(Group group);

        /// <summary>
        /// Gets all groups.
        /// </summary>
        /// <returns>List of all groups</returns>
        Task<List<Group>> GetAllGroups();

        /// <summary>
        /// Gets a group by Id.
        /// </summary>
        /// <param name="id">The Id of the group to retrieve.</param>
        /// <returns>The group with the corresponding Id.</returns>
        Task<Group?> GetGroupById(Guid id);

        /// <summary>
        /// Gets all groups associated with a user's email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>List of all groups associated with the user's email.</returns>
        Task<List<Group>> GetGroupsByUserEmail(string email);

        /// <summary>
        /// Gets all groups associated with a user's Id.
        /// </summary>
        /// <param name="userId">The Id of the user.</param>
        /// <returns>List of all groups associated with the user's Id.</returns>
        Task<List<Group>> GetGroupsByUserId(Guid userId);

        /// <summary>
        /// Updates a group.
        /// </summary>
        /// <param name="group">The group to update.</param>
        /// <returns>The updated group.</returns>
        Task<Group> UpdateGroup(Group group);
    }
}