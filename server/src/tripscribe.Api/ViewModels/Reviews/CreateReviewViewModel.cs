using FluentValidation;

namespace tripscribe.Api.ViewModels.Reviews;

public class CreateReviewViewModel
{
    public string ReviewText { get; set; }
    public int Score { get; set; }
    public DateTime Timestamp { get; set; }
}

public class CreateReviewValidator : AbstractValidator<CreateReviewViewModel>
{
    public CreateReviewValidator()
    {
        RuleFor(rev => rev.ReviewText).MaximumLength(2000).WithMessage("Review has a maximum character limit of 2000 characters");
        RuleFor(rev => rev.Score).GreaterThan(0).LessThanOrEqualTo(5).WithMessage("Score must be between 1 and 5");
        RuleFor(rev => rev.Timestamp).LessThanOrEqualTo(DateTime.Now);
    }
}