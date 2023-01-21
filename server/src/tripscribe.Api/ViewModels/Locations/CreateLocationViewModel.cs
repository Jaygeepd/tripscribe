using System.Drawing;
using FluentValidation;

namespace tripscribe.Api.ViewModels.Locations;

public class CreateLocationViewModel
{
    public string Name { get; set; }
    public DateTime DateArrived { get; set; }
    public string LocationType { get; set; }
    
    public Point GeoLocation { get; set; }
    public int StopId { get; set; }
}

public class CreateLocationValidator : AbstractValidator<CreateLocationViewModel>
{
    public CreateLocationValidator()
    {
        RuleFor(loc => loc.Name).NotNull().Length(1, 100).WithMessage("Name must be entered for this location");
        RuleFor(loc => loc.DateArrived).NotNull().WithMessage("Date arrived must be chosen");
        RuleFor(loc => loc.LocationType).NotNull().Length(1, 30).WithMessage("Location type chosen was invalid");
        RuleFor(loc => loc.StopId).NotNull().NotEmpty().WithMessage("Stop ID must be selected");
    }
}