using ExpenSpend.Core.DTOs.Accounts;
using ExpenSpend.Core.DTOs.Users;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace ExpenSpend.Domain.Models.Users
{
    public interface IAuthAppService
    {
        Task<UserRegistrationResult> RegisterUserAsync(CreateUserDto input);
        Task<IdentityResult> ConfirmEmailAsync(ESUser user, string token);
        Task<string> GenerateEmailConfirmationTokenAsync(ESUser user);
        Task<string> GenerateResetToken(ESUser user);
        Task<SignInResult> LoginUserAsync(string email, string password);
        Task<JwtSecurityToken> LoginUserJwtAsync(string userName, string password, bool rememberMe);
        Task LogoutUserAsync();
        Task<IdentityResult> RegisterUserAsync(ESUser user, string password);
        Task<IdentityResult> ResetPasswordAsync(ESUser user, string token, string newPassword);
    }
}
