using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenSpend.Core.DTOs.Accounts;
using ExpenSpend.Core.DTOs.Accounts.Const;
using ExpenSpend.Core.DTOs.Users;
using ExpenSpend.Domain.Models.Users;
using ExpenSpend.Service.Emails.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenSpend.Web.Controllers;

[Route("api/account")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IAuthAppService _authService;
    private readonly IUserAppService _userService;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    public AuthController(IAuthAppService authService, IMapper mapper, IEmailService emailService, IUserAppService userService)
    {
        _authService = authService;
        _mapper = mapper;
        _emailService = emailService;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(CreateUserDto input)
    {
        var registrationResult = await _authService.RegisterUserAsync(input);

        if (registrationResult.IsSuccess)
        {
            var user = await _userService.GetUserByEmailAsync(input.Email);
            await SendEmailConfAsync(user);
            return Ok(AccConsts.RegSuccessMessage);
        }

        return BadRequest(registrationResult.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync(LoginDto login)
    {
        var userToken = await _authService.LoginUserJwtAsync(login.Email, login.Password, login.RememberMe);
        if (userToken != null)
        {
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(userToken) });
        }
        return Unauthorized();
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutUserAsync()
    {
        await _authService.LogoutUserAsync();
        return Ok();
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);
        if (user == null)
        {
            return BadRequest(AccConsts.UserNotFound);
        }

        var resetToken = await _authService.GenerateResetToken(user);
        var resetLink = Url.Action(nameof(ResetPassword), "Account", new { token = resetToken, email = user.Email }, Request.Scheme);

        _emailService.SendPasswordResetEmail(user.Email!, resetLink!);

        return Ok(AccConsts.PasswordResetReqSuccess);
    }

    [HttpGet("reset-password")]
    public IActionResult ResetPassword(string token, string email)
    {
        var model = new ResetPasswordDto { Token = token, Email = email };
        return Ok(model);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userService.GetUserByEmailAsync(resetPasswordDto.Email);
        if (user == null)
        {
            return BadRequest(AccConsts.UserNotFound);
        }

        var resetPasswordResult = await _authService.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
        if (resetPasswordResult.Succeeded)
        {
            _emailService.SandPasswordChangeNotification(user.Email!, user.FirstName);
            return Ok();
        }

        resetPasswordResult.Errors.ToList().ForEach(error => ModelState.AddModelError(error.Code, error.Description));
        return BadRequest(ModelState);
    }

    [HttpGet("confirm-email")]
    public async Task<ContentResult> ConfirmEmail(string token, string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);

        if (user == null)
        {
            return Content(AccConsts.UserNotFound);
        }

        var emailConfirmationResult = await _authService.ConfirmEmailAsync(user, token);

        if (emailConfirmationResult.Succeeded)
        {
            var htmlContent = await _emailService.EmailConfirmationPageTemplate();

            return Content(htmlContent, "text/html");
        }

        emailConfirmationResult.Errors.ToList().ForEach(error => ModelState.AddModelError(error.Code, error.Description));
        return Content(ModelState.ToString()!);
    }
    private async Task SendEmailConfAsync(ESUser user)
    {
        var emailConfirmationToken = await _authService.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { token = emailConfirmationToken, email = user.Email }, Request.Scheme);
        var emailMessage = await _emailService.CreateEmailValidationTemplateMessage(user.Email!, confirmationLink!);
        _emailService.SendEmail(emailMessage);
    }
}