using ExpenSpend.Core.DTOs.Users;
using ExpenSpend.Core.Helpers;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace ExpenSpend.Domain.Models.Users;

/// <summary>
/// Interface for user-related application services.
/// </summary>
public interface IUserAppService
{
    /// <summary>
    /// Retrieves the currently logged-in user.
    /// </summary>
    /// <returns>The logged-in user's data.</returns>
    Task<GetUserDto> GetLoggedInUser();

    /// <summary>
    /// Deletes a user by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    /// <returns>An API response indicating the result of the deletion operation.</returns>
    Task<ApiResponse<GetUserDto>> DeleteUserAsync(Guid id);

    /// <summary>
    /// Retrieves a list of all users asynchronously.
    /// </summary>
    /// <returns>A list of user data transfer objects.</returns>
    Task<List<GetUserDto>> GetAllUsersAsync();

    /// <summary>
    /// Retrieves a user by their email address asynchronously.
    /// </summary>
    /// <param name="email">The email address of the user to retrieve.</param>
    /// <returns>The user associated with the provided email.</returns>
    Task<ESUser?> GetUserByEmailAsync(string email);

    /// <summary>
    /// Retrieves a user by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <returns>The user associated with the provided unique identifier.</returns>
    Task<ESUser> GetUserByIdAsync(string id);

    /// <summary>
    /// Retrieves a user by their username asynchronously.
    /// </summary>
    /// <param name="userName">The username of the user to retrieve.</param>
    /// <returns>The user associated with the provided username.</returns>
    Task<ESUser> GetUserByUserNameAsync(string userName);

    /// <summary>
    /// Updates a user's information asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update.</param>
    /// <param name="user">The updated user data.</param>
    /// <returns>An API response indicating the result of the update operation.</returns>
    Task<ApiResponse<GetUserDto>> UpdateUserAsync(Guid id, UpdateUserDto user);
}
