using System.ComponentModel.DataAnnotations;

namespace ExpenSpend.Core.DTOs.Accounts;

public class ResetPasswordDto
{
    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 8 and 15 characters and contain at least one uppercase letter, one lowercase letter, one digit and one special character.")]
    public string Password { get; set; } = null!;

    [Required]
    [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
    public string ConfirmPassword { get; set; } = null!;

    [EmailAddress]
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Token { get; set; } = null!;
}