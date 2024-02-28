using System.ComponentModel.DataAnnotations;

namespace ExpenSpend.Core.DTOs.Groups;
public class CreateGroupDto
{
    [Required]
    public string Name { get; set; }
    public string? About { get; set; }
}
