using System.ComponentModel.DataAnnotations;

namespace ExpenSpend.Domain.DTOs.Groups;
public class CreateGroupDto
{
    public required string Name { get; set; }
    public string? About { get; set; }
}
