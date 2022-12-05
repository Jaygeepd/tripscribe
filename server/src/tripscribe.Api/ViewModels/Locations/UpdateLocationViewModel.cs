using FluentValidation;

namespace tripscribe.Api.ViewModels.Locations;

public class UpdateLocationViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateArrived { get; set; }
    public string LocationType { get; set; }
}

public class UpdateLocationValidator : AbstractValidator<CreateLocationViewModel>
{
    public UpdateLocationValidator()
    {
        RuleFor(loc => loc).Must(loc => loc.Name != null  && loc.LocationType != null)
            .WithMessage("At least one value required").WithName("NoValue");
        
        When(loc => loc.Name != null, () =>
        {
            RuleFor(loc => loc.Name).NotNull().Length(1, 100).WithMessage("Name must be entered for this location");
        });
        
        When(loc => loc.LocationType != null, () =>
        {
            RuleFor(loc => loc.LocationType).NotNull().Length(1, 100).WithMessage("Location type chosen was invalid");
        });
    }
}