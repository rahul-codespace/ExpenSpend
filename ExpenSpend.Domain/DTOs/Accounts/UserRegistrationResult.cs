using Microsoft.AspNetCore.Identity;

namespace ExpenSpend.Domain.DTOs.Accounts
{
    public class UserRegistrationResult
    {
        public bool IsSuccess { get; set; }
        public List<RegistrationError>? Errors { get; set; }

        public static UserRegistrationResult Success()
        {
            return new UserRegistrationResult { IsSuccess = true };
        }

        public static UserRegistrationResult Failure<T>(List<IdentityError> errors)
        {
            var registrationErrors = errors.Select(error => new RegistrationError { Code = error.Code, Description = error.Description }).ToList();
            return new UserRegistrationResult { IsSuccess = false, Errors = registrationErrors };
        }
        public static UserRegistrationResult UserExistsError()
        {
            return new UserRegistrationResult { IsSuccess = false, Errors = new List<RegistrationError> { new RegistrationError { Code = "UserExists", Description = "User with this email already exists." } } };
        }
    }
    public class RegistrationError
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
    }

}
