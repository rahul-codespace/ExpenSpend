using ExpenSpend.Domain.Models.GroupMembers;

namespace ExpenSpend.Repository.GroupMembers
{
    /// <summary>
    /// Interface for managing group members in the application.
    /// </summary>
    public interface IGroupMemberRepository
    {
        /// <summary>
        /// Creates a new group member.
        /// </summary>
        /// <param name="groupMember">The group member object to be created.</param>
        /// <returns>The newly created group member object.</returns>
        Task<GroupMember> CreateGroupMember(GroupMember groupMember);

        Task<List<GroupMember>> CreateGroupMembers(List<GroupMember> groupMembers);

        Task<GroupMember> SoftDeleteGroupMember(GroupMember groupMember);

        /// <summary>
        /// Deletes an existing group member.
        /// </summary>
        /// <param name="groupMember">The group member object to be deleted.</param>
        /// <returns>Task object representing the asynchronous operation to delete the group member.</returns>
        Task DeleteGroupMember(GroupMember groupMember);

        /// <summary>
        /// Gets all group members.
        /// </summary>
        /// <returns>List of all group member objects.</returns>
        Task<List<GroupMember>> GetAllGroupMembers();

        /// <summary>
        /// Gets a specific group member by its Id.
        /// </summary>
        /// <param name="id">The id of the group member to retrieve.</param>
        /// <returns>The group member object with the corresponding id or null.</returns>
        Task<GroupMember?> GetGroupMemberById(Guid id);

        /// <summary>
        /// Gets all group members belonging to a specified group.
        /// </summary>
        /// <param name="groupId">The id of the group whose members are to be retrieved.</param>
        /// <returns>List of all group member objects belonging to the specified group.</returns>
        Task<List<GroupMember>> GetGroupMembersByGroupId(Guid groupId);

        /// <summary>
        /// Gets all group members belonging to a specified user.
        /// </summary>
        /// <param name="userId">The id of the user whose group membership is to be retrieved.</param>
        /// <returns>List of all group member objects to which the specified user belongs.</returns>
        Task<List<GroupMember>> GetGroupMembersByUserId(Guid userId);

        /// <summary>
        /// Updates an existing group member.
        /// </summary>
        /// <param name="groupMember">The group member object to be updated.</param>
        /// <returns>The updated group member object.</returns>
        Task<GroupMember> UpdateGroupMember(GroupMember groupMember);

        Task<bool> CheckExistingGroupMemberAsync(Guid groupId, Guid userId);
    }
}