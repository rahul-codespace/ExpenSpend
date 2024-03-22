using System.ComponentModel.DataAnnotations;
namespace ExpenSpend.Domain.DTOs.GroupMembers;

public class UpdateGroupMemberDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public bool IsAdmin { get; set; } = false;
}
