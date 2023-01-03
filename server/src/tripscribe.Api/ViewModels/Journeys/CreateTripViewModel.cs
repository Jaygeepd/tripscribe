using FluentValidation;

namespace tripscribe.Api.ViewModels.Journeys;

public class CreateTripViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public class CreateTripValidator : AbstractValidator<CreateTripViewModel>
{
    public CreateTripValidator()
    {
        RuleFor(jour => jour.Title)
            .NotNull().WithMessage("Title must be not null")
            .Length(5, 100).WithMessage("Title must be between 5 and 100 characters in length");
        RuleFor(jour => jour.Description).MaximumLength(2000).WithMessage("Description cannot be longer than 2000 characters");
    }
}