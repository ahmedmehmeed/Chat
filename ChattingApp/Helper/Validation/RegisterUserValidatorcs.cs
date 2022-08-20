
using ChattingApp.Resource.Account;
using FluentValidation;

namespace ChattingApp.Helper.Validation
{
    public class RegisterUserValidatorcs : AbstractValidator<RegisterDto>
    {
        public RegisterUserValidatorcs()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("field is required")
                .NotNull().WithMessage("field is required");
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("field is required")
                .NotNull().WithMessage("field is required")
                .MinimumLength(8)
                .Matches("[A-Z]").WithMessage("Password must contain one or more capital letters.")
                .Matches("[a-z]").WithMessage("Password must contain one or more lowercase letters.")
                .Matches(@"\d").WithMessage("Password must contain one or more digits.")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("'Password must contain one or more special characters.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("field is required")
                .NotNull().WithMessage("field is required")
                .EmailAddress().WithMessage("Invalid Email Address");





        }
    }
}
