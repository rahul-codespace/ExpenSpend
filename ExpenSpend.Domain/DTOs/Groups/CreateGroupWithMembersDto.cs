namespace ExpenSpend.Core.DTOs.Groups;

public class CreateGroupWithMembersDto
{
    public string Name { get; set; } = null!;
    public string? About { get; set; }
    public List<Guid> MemberIds { get; set; } = null!;
}
