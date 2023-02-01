using System.Drawing;
using FluentValidation;

namespace tripscribe.Api.ViewModels.Locations;

public class UpdateLocationViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateArrived { get; set; }
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
    public string LocationType { get; set; }
}

public class UpdateLocationValidator : AbstractValidator<UpdateLocationViewModel>
{
    public UpdateLocationValidator()
    {
        RuleFor(loc => loc).Must(loc => loc.Name != null  && loc.LocationType != null)
            .WithMessage("At least one value required").WithName("NoValue");

        When(loc => loc.Name != null, () => 
                RuleFor(loc => loc.Name).Length(4, 100).WithMessage("Name must be between 4 and 100 characters in length")
            );

        When(loc => loc.LocationType != null, () => 
                RuleFor(loc => loc.LocationType).Length(4, 100).WithMessage("Location type must be between 4 and 100 characters in length")
            );
        
        
       
    }
}