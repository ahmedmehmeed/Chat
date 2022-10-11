
using ChattingApp.Resource.Account;
using FluentValidation;

namespace ChattingApp.Helper.Validation
{
    public class RegisterUserValidatorcs : AbstractValidator<RegisterDto>
    {
        public RegisterUserValidatorcs()
        {
            RuleFor(p=>p.FirstName)
                .NotEmpty().WithMessage("FirstName is required")
                .NotNull().WithMessage("FirstName is required");
            RuleFor(p=>p.LastName)
                .NotEmpty().WithMessage("LastName is required")
                .NotNull().WithMessage("LastName is required");
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("UserName is required")
                .NotNull().WithMessage("UserName is required");
            RuleFor(p => p.KnownAs)
                .NotEmpty().WithMessage("KnownAs is required")
                .NotNull().WithMessage("KnownAs is required");
           RuleFor(p => p.BirthDate)
                .NotEmpty().WithMessage("BirthDate is required")
                .NotNull().WithMessage("BirthDate is required");
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is required")
                .NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid Email Address");
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password is required")
                .NotNull().WithMessage("Password is required")
                .MinimumLength(8)
                .Matches("[A-Z]").WithMessage("Password must contain one or more capital letters.")
                .Matches("[a-z]").WithMessage("Password must contain one or more lowercase letters.")
                .Matches(@"\d").WithMessage("Password must contain one or more digits.")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("'Password must contain one or more special characters.");






        }
    }
}
