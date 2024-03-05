using System.ComponentModel.DataAnnotations;
namespace ExpenSpend.Core.DTOs.GroupMembers;

public class UpdateGroupMemberDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public bool IsAdmin { get; set; } = false;
}
