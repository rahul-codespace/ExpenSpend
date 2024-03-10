using ExpenSpend.Domain.DTOs.Users;
using ExpenSpend.Domain.DTOs.Accounts;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace ExpenSpend.Domain.Models.Users
{
    /// <summary>
    /// Provides authentication and user registration functionality.
    /// </summary>
    public interface IAuthAppService
    {
        /// <summary>
        /// Registers a new user asynchronously using the provided user data.
        /// </summary>
        /// <param name="input">The user data to register.</param>
        /// <returns>The result of the user registration operation.</returns>
        Task<UserRegistrationResult> RegisterUserAsync(CreateUserDto input);

        /// <summary>
        /// Confirms a user's email address asynchronously using a token.
        /// </summary>
        /// <param name="user">The user whose email is being confirmed.</param>
        /// <param name="token">The email confirmation token.</param>
        /// <returns>The result of the email confirmation operation.</returns>
        Task<IdentityResult> ConfirmEmailAsync(ESUser? user, string token);

        /// <summary>
        /// Generates an email confirmation token for a user asynchronously.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <returns>The generated email confirmation token.</returns>
        Task<string> GenerateEmailConfirmationTokenAsync(ESUser? user);

        /// <summary>
        /// Generates a password reset token for a user asynchronously.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <returns>The generated password reset token.</returns>
        Task<string> GenerateResetToken(ESUser? user);

        /// <summary>
        /// Logs in a user asynchronously using their email and password.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>The result of the login operation.</returns>
        Task<SignInResult> LoginUserAsync(string email, string password);

        /// <summary>
        /// Logs in a user asynchronously and generates a JWT for authentication.
        /// </summary>
        /// <param name="userName">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="rememberMe">Indicates whether to remember the user's login.</param>
        /// <returns>The generated JWT token.</returns>
        Task<JwtSecurityToken?> LoginUserJwtAsync(string userName, string password, bool rememberMe);

        /// <summary>
        /// Logs out the currently authenticated user asynchronously.
        /// </summary>
        Task LogoutUserAsync();
        Task<IdentityResult> RegisterUserAsync(ESUser? user, string password);

        /// <summary>
        /// Resets a user's password asynchronously.
        /// </summary>
        /// <param name="user">The user whose password is being reset.</param>
        /// <param name="token">The reset token.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>The result of the password reset operation.</returns>
        Task<IdentityResult> ResetPasswordAsync(ESUser? user, string token, string newPassword);
    }
}
