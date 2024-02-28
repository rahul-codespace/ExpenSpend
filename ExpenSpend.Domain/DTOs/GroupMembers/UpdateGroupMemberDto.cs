namespace ExpenSpend.Core.DTOs.GroupMembers;

public class UpdateGroupMemberDto
{
    public Guid Id { get; set; }
    public bool IsAdmin { get; set; } = false;
}
