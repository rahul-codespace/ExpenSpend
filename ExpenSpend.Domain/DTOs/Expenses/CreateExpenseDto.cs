using ExpenSpend.Domain.Models.Expenses;

namespace ExpenSpend.Domain.DTOs.Expenses;

public class CreateExpenseDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Guid GroupId { get; set; }
    public double Amount { get; set; }
    public SplitAs SplitAs { get; set; } = SplitAs.Equally;
    public bool IsSettled { get; set; }
}
