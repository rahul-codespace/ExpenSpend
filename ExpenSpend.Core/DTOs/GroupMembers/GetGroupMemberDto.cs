using ExpenSpend.Core.DTOs.Groups;
using ExpenSpend.Core.DTOs.Users;

namespace ExpenSpend.Core.DTOs.GroupMembers;

public class GetGroupMemberDto
{
    public Guid Id { get; set; }
    public Guid GroupId { get; set; }
    public GetGroupDto? Group { get; set; }
    public Guid UserId { get; set; }
    public GetUserDto? User { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}
