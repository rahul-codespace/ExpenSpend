using ExpenSpend.Domain.DTOs.Expenses;
using ExpenSpend.Service.Models;

public interface IExpenseAppService
{
    /// <summary>
    /// Retrieves all expenses.
    /// </summary>
    /// <returns>A response containing a list of expenses.</returns>
    Task<Response> GetAllExpensesAsync();

    /// <summary>
    /// Retrieves an expense by its ID.
    /// </summary>
    /// <param name="id">The ID of the expense to retrieve.</param>
    /// <returns>A response containing the expense details.</returns>
    Task<Response> GetExpenseByIdAsync(Guid id);

    /// <summary>
    /// Retrieves the payments associated with a specific expense.
    /// </summary>
    /// <param name="id">The ID of the expense.</param>
    /// <returns>A response containing a list of payments for the expense.</returns>
    Task<Response> GetExpensePaymentsAsync(Guid id);

    /// <summary>
    /// Retrieves all expenses for a specific group.
    /// </summary>
    /// <param name="groupId">The ID of the group.</param>
    /// <returns>A response containing a list of expenses for the group.</returns>
    Task<Response> GetGroupExpensesAsync(Guid groupId);

    /// <summary>
    /// Retrieves all expenses for a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A response containing a list of expenses for the user.</returns>
    Task<Response> GetUserExpensesAsync(Guid userId);

    /// <summary>
    /// Retrieves all expenses for a specific user within a specific group.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="groupId">The ID of the group.</param>
    /// <returns>A response containing a list of expenses for the user within the group.</returns>
    Task<Response> GetUserGroupExpensesAsync(Guid userId, Guid groupId);

    /// <summary>
    /// Creates a new expense.
    /// </summary>
    /// <param name="createExpenseDto">The data transfer object containing the details of the expense to create.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response> CreateExpenseAsync(CreateExpenseDto createExpenseDto);

    /// <summary>
    /// Updates an existing expense.
    /// </summary>
    /// <param name="id">The ID of the expense to update.</param>
    /// <param name="updateExpenseDto">The data transfer object containing the updated details of the expense.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response> UpdateExpenseAsync(Guid id, UpdateExpenseDto updateExpenseDto);

    /// <summary>
    /// Deletes an expense.
    /// </summary>
    /// <param name="id">The ID of the expense to delete.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response> DeleteExpenseAsync(Guid id);

    /// <summary>
    /// Settles an expense.
    /// </summary>
    /// <param name="id">The ID of the expense to settle.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    Task<Response> SettleExpenseAsync(Guid id);
}
