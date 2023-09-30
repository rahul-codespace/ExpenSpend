using ExpenSpend.Domain.Models.GroupMembers;
namespace ExpenSpend.Domain.Models.Groups;

public class Group : BaseEntity
{
    public required string Name { get; set; }
    public string? About { get; set; }
    public List<GroupMember>? Members { get; set; }
}
