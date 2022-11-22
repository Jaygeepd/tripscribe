using FluentValidation;

namespace tripscribe.Api.ViewModels.Journeys;

public class CreateJourneyViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public class CreateJourneyValidator : AbstractValidator<CreateJourneyViewModel>
{
    public CreateJourneyValidator()
    {
        RuleFor(jour => jour.Title).NotNull().Length(5, 100).WithMessage("Title must be entered, and between 5 and 100 characters in length");
        RuleFor(jour => jour.Description).MaximumLength(2000).WithMessage("Description cannot be longer than 2000 characters");
    }
}