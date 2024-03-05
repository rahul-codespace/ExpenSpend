using System.ComponentModel.DataAnnotations;

namespace ExpenSpend.Core.DTOs.GroupMembers;

public class CreateGroupMemberDto
{
    [Required]
    public Guid GroupId { get; set; }
    [Required]
    public Guid UserId { get; set; }
}
