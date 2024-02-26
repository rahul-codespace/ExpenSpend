using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using AutoMapper;
using ExpenSpend.Core.DTOs.Accounts;
using ExpenSpend.Core.DTOs.Users;
using ExpenSpend.Domain.Models.Users;
using ExpenSpend.Service.Emails.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ExpenSpend.Service
{
    public class AuthAppService : IAuthAppService
    {
        private readonly UserManager<ESUser?> _userManager;
        private readonly SignInManager<ESUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthAppService(
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            IEmailService emailService,
            UserManager<ESUser?> userManager,
            SignInManager<ESUser> signInManager,
            IConfiguration configuration
        ){
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _emailService = emailService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<UserRegistrationResult> RegisterUserAsync(CreateUserDto input)
        {
            var userExists = await _userManager.FindByNameAsync(input.Email);
            if (userExists != null)
            {
                return UserRegistrationResult.UserExistsError();
            }

            var user = _mapper.Map<ESUser>(input);
            user.UserName = input.Email;
            var registrationResult = await RegisterUserAsync(user, input.Password);

            if (registrationResult.Succeeded)
            {
                return UserRegistrationResult.Success();
            }
            else
            {
                return UserRegistrationResult.Failure<List<IdentityError>>(registrationResult.Errors.ToList());
            }
            
        }

        public async Task<IdentityResult> RegisterUserAsync(ESUser? user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        public async Task<SignInResult> LoginUserAsync(string email, string password)
        {
            return await _signInManager.PasswordSignInAsync(email, password, false, false);
        }
        public async Task<JwtSecurityToken?> LoginUserJwtAsync(string userName, string password, bool rememberMe)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return null;
            }

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new("FirstName",user.FirstName),
                new(ClaimTypes.Surname, user.LastName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange((await _userManager.GetRolesAsync(user)).Select(role => new Claim(ClaimTypes.Role, role)));
            var expirationTime = rememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddHours(8);
            return GenerateTokenOptions(authClaims, expirationTime);
        }
        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> ResetPasswordAsync(ESUser? user, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }
        public async Task<string> GenerateEmailConfirmationTokenAsync(ESUser? user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
        public async Task<IdentityResult> ConfirmEmailAsync(ESUser? user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }
        public async Task<string> GenerateResetToken(ESUser? user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        // Private method to generate JWT token options...
        private JwtSecurityToken? GenerateTokenOptions(List<Claim> authClaims, DateTime expires)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!);
            var tokenOptions = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        claims: authClaims,
                        expires: expires,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
            return tokenOptions;
        }
    }
}
