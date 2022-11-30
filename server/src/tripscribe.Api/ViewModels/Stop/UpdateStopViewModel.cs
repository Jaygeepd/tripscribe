using FluentValidation;

namespace tripscribe.Api.ViewModels.Stop;

public class UpdateStopViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateArrived { get; set; }
    public DateTime DateDeparted { get; set; }
}

public class UpdateStopValidator : AbstractValidator<UpdateStopViewModel>
{
    public UpdateStopValidator()
    {
        RuleFor(stop => stop.Name).NotNull().Length(4, 100)
            .WithMessage("Stop name be entered, and between 4 and 100 characters in length");
        RuleFor(stop => stop.DateArrived).NotNull();
        RuleFor(stop => stop.DateDeparted).NotNull();
    }
}