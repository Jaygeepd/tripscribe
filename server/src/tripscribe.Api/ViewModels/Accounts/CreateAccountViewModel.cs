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
        RuleFor(acc => acc.FirstName).NotNull().Length(1, 100).WithMessage("First name must be entered, and under 100 characters in length");
        RuleFor(acc => acc.LastName).NotNull().Length(1, 100).WithMessage("Last name must be entered, and under 100 characters in length");
        RuleFor(acc => acc.Email).EmailAddress().NotNull().WithMessage("Invalid Email Entered");
        RuleFor(acc => acc.Password).NotNull().Length(8, 30)
            .WithMessage("Password must be between 8 and 30 characters");
    }
}
