using FluentValidation;

namespace tripscribe.Api.ViewModels.Accounts;

public class UpdateAccountViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class UpdateAccountValidator : AbstractValidator<UpdateAccountViewModel>
{
    public UpdateAccountValidator()
    {
        RuleFor(acc => acc.FirstName).NotNull().Length(1, 100).WithMessage("First name must be entered, and under 100 characters in length");
        RuleFor(acc => acc.LastName).NotNull().Length(1, 100).WithMessage("Last name must be entered, and under 100 characters in length");
    }
}
