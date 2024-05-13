namespace ExpenSpend.Domain.DTOs.Payments;

public class CreatePaymentDto
{
    public Guid OwenedById { get; set; }
    public Guid ExpenseId { get; set; }
    public double Amount { get; set; }
}
