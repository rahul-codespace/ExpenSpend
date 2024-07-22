using ExpenSpend.Domain.DTOs.Payments;
using ExpenSpend.Service.Models;

namespace ExpenSpend.Service.Contracts
{

    /// <summary>
    /// Interface for managing payments.
    /// </summary>
    public interface IPaymentAppService
    {
        /// <summary>
        /// Retrieves all payments.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a Response containing a list of GetPaymentDto.</returns>
        Task<Response> GetAllPaymentsAsync();

        /// <summary>
        /// Retrieves a payment by its ID.
        /// </summary>
        /// <param name="id">The ID of the payment to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns a Response containing a GetPaymentDto.</returns>
        Task<Response> GetPaymentByIdAsync(Guid id);

        /// <summary>
        /// Retrieves payments by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A task representing the asynchronous operation that returns a Response containing a list of GetPaymentDto.</returns>
        Task<Response> GetPaymentsByUserIdAsync(Guid userId);

        /// <summary>
        /// Creates a new payment.
        /// </summary>
        /// <param name="input">The input data for creating the payment.</param>
        /// <returns>A task representing the asynchronous operation that returns a Response containing the created payment.</returns>
        Task<Response> CreatePaymentAsync(CreatePaymentDto input);

        /// <summary>
        /// Updates an existing payment.
        /// </summary>
        /// <param name="id">The ID of the payment to update.</param>
        /// <param name="input">The input data for updating the payment.</param>
        /// <returns>A task representing the asynchronous operation that returns a Response containing the updated payment.</returns>
        Task<Response> UpdatePaymentAsync(Guid id, UpdatePaymentDto input);

        /// <summary>
        /// Deletes a payment by its ID.
        /// </summary>
        /// <param name="id">The ID of the payment to delete.</param>
        /// <returns>A task representing the asynchronous operation that returns a Response indicating the result of the deletion.</returns>
        Task<Response> DeletePaymentAsync(Guid id);

        /// <summary>
        /// Sets a payment as settled.
        /// </summary>
        /// <param name="id">The ID of the payment to settle.</param>
        /// <returns>A task representing the asynchronous operation that returns a Response containing the settled payment.</returns>
        Task<Response> SettlePaymentAsync(Guid id);
    }
}
