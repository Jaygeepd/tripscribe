using FluentValidation;

namespace tripscribe.Api.ViewModels.Trips;

public class UpdateTripViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public Boolean PublicView { get; set; }
}

public class UpdateTripValidator : AbstractValidator<UpdateTripViewModel>
{
    public UpdateTripValidator()
    {
        RuleFor(jour => jour.Title)
            .NotNull().WithMessage("Title must be not null")
            .Length(5, 100).WithMessage("Title must be between 5 and 100 characters in length");
        RuleFor(jour => jour.Description).MaximumLength(2000).WithMessage("Description cannot be longer than 2000 characters");
    }
}