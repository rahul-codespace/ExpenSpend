using ExpenSpend.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace ExpenSpend.Repository.Account;

public interface IAccountRepository
{
    /// <summary>
    /// Registers a new user with the provided password.
    /// </summary>
    /// <param name="user">The user model.</param>
    /// <param name="password">ESUser's password.</param>
    /// <returns>Returns an IdentityResult indicating the outcome of the registration process.</returns>
    Task<IdentityResult> RegisterUserAsync(ESUser user, string password);

    /// <summary>
    /// Attempts to sign in the user using the provided email and password.
    /// </summary>
    /// <param name="email">ESUser's email.</param>
    /// <param name="password">ESUser's password.</param>
    /// <returns>Returns a SignInResult indicating the outcome of the login attempt.</returns>
    Task<SignInResult> LoginUserAsync(string email, string password);

    /// <summary>
    /// Logs out the current user.
    /// </summary>
    Task LogoutUserAsync();

    /// <summary>
    /// Finds a user based on the provided username.
    /// </summary>
    /// <param name="userName">ESUser's name.</param>
    /// <returns>Returns the user if found, otherwise null.</returns>
    Task<ESUser?> FindByUserNameAsync(string userName);

    /// <summary>
    /// Finds a user based on the provided email.
    /// </summary>
    /// <param name="email">ESUser's email.</param>
    /// <returns>Returns the user if found, otherwise null.</returns>
    Task<ESUser?> FindByEmail(string email);

    /// <summary>
    /// Resets the password for a given user using a token.
    /// </summary>
    /// <param name="user">The user model.</param>
    /// <param name="token">The token for password reset.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns>Returns an IdentityResult indicating the outcome of the reset process.</returns>
    Task<IdentityResult> ResetPasswordAsync(ESUser user, string token, string newPassword);

    /// <summary>
    /// Generates an email confirmation token for the given user.
    /// </summary>
    /// <param name="user">The user model.</param>
    /// <returns>Returns the generated email confirmation token.</returns>
    Task<string> GenerateEmailConfirmationTokenAsync(ESUser user);

    /// <summary>
    /// Confirms the user's email address using a token.
    /// </summary>
    /// <param name="user">The user model.</param>
    /// <param name="token">The email confirmation token.</param>
    /// <returns>Returns an IdentityResult indicating the outcome of the email confirmation process.</returns>
    Task<IdentityResult> ConfirmEmailAsync(ESUser user, string token);

    /// <summary>
    /// Generates a password reset token for the given user.
    /// </summary>
    /// <param name="user">The user model.</param>
    /// <returns>Returns the generated password reset token.</returns>
    Task<string> GenerateResetToken(ESUser user);

    /// <summary>
    /// Generates a JWT for a valid user based on username and password.
    /// </summary>
    /// <param name="userName">ESUser's name.</param>
    /// <param name="password">ESUser's password.</param>
    /// <returns>Returns a JWT if the user is valid, otherwise returns null.</returns>
    Task<JwtSecurityToken> LoginUserJwtAsync(string userName, string password, bool rememberMe);
}