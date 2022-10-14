using ChattingApp.Resource.User;
using FluentValidation;

namespace ChattingApp.Helper.Validation
{
    public class UserUpdateDtoValidator:AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(p => p.FirstName)
              .NotEmpty().WithMessage("FirstName is required")
              .NotNull().WithMessage("FirstName is required");
            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("LastName is required")
                .NotNull().WithMessage("LastName is required");
            RuleFor(p => p.KnownAs)
                .NotEmpty().WithMessage("KnownAs is required")
                .NotNull().WithMessage("KnownAs is required");
            RuleFor(p => p.BirthDate)
                 .NotEmpty().WithMessage("BirthDate is required")
                 .NotNull().WithMessage("BirthDate is required");
            RuleFor(p => p.City)
                 .NotEmpty().WithMessage("City is required")
                 .NotNull().WithMessage("City is required");
            RuleFor(p => p.Country)
                 .NotEmpty().WithMessage("City is required")
                 .NotNull().WithMessage("City is required");
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is required")
                .NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid Email Address");
        }
    }
}
