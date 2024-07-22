using ExpenSpend.Domain.Models;
using ExpenSpend.Domain.Models.Expenses;
using ExpenSpend.Domain.Models.Users;

namespace ExpenSpend.Domain.DTOs.Payments;

public class GetPaymentDto : BaseEntity
{
    public Guid OwenedById { get; set; }
    public Guid ExpenseId { get; set; }
    public double Amount { get; set; }
    public bool IsSettled { get; set; }
}
