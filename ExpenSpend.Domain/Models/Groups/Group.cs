using ExpenSpend.Domain.Models.Expenses;
using ExpenSpend.Domain.Models.GroupMembers;
namespace ExpenSpend.Domain.Models.Groups;

public class Group : BaseEntity
{
    public required string Name { get; set; }
    public string? About { get; set; }
    public List<GroupMember>? Members { get; set; }
    public List<Expense>? Expenses { get; set; }
}
