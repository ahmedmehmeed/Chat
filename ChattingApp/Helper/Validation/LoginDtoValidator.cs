using ChattingApp.Resource.Account;
using FluentValidation;

namespace ChattingApp.Helper.Validation
{
    public class LoginDtoValidator: AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is required!")
                .NotNull().WithMessage("Email is required!");
            RuleFor(p=>p.password)
                .NotEmpty().WithMessage("password is required!")
                .NotNull().WithMessage("password is required!");
        }
    }
}
