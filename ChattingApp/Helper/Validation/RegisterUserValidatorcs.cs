
using ChattingApp.Resource.Account;
using FluentValidation;

namespace ChattingApp.Helper.Validation
{
    public class RegisterUserValidatorcs:AbstractValidator<RegisterDto>
    {
        public RegisterUserValidatorcs()
        {
            RuleFor(p => p.name)
                .NotEmpty().WithMessage("field is required")
                .NotNull().WithMessage("field is required");


        }
    }
}
