using FluentValidation;

namespace tripscribe.Api.ViewModels.Accounts;

public class CreateAccountViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class CreateAccountValidator : AbstractValidator<CreateAccountViewModel>
{
    public CreateAccountValidator()
    {
        RuleFor(acc => acc.FirstName)
            .NotNull().WithMessage("First name must be entered")
            .Length(2, 100).WithMessage("First name must be between 2 and 100 characters in length");
        RuleFor(acc => acc.LastName)
            .NotNull().WithMessage("Last name must be entered")
            .Length(2, 100).WithMessage("Last name must be between 2 and 100 characters in length");
        RuleFor(acc => acc.Email)
            .EmailAddress().WithMessage("Invalid email address")
            .NotNull().WithMessage("Email must be entered");
        RuleFor(acc => acc.Password)
            .NotNull().WithMessage("Password must be entered")
            .Length(8, 30).WithMessage("Password must be between 8 and 30 characters");
    }
}
