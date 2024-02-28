namespace ExpenSpend.Core.DTOs.GroupMembers;

public class CreateGroupMemberDto
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
}
