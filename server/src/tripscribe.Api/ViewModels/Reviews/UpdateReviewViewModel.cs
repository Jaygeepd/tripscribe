using FluentValidation;

namespace tripscribe.Api.ViewModels.Reviews;

public class UpdateReviewViewModel
{
    public string ReviewText { get; set; }
    public int Score { get; set; }
}

public class UpdateReviewValidator : AbstractValidator<UpdateReviewViewModel>
{
    public UpdateReviewValidator()
    {
        RuleFor(rev => rev.ReviewText).MaximumLength(2000).WithMessage("Review has a maximum character limit of 2000 characters");
        RuleFor(rev => rev.Score).GreaterThan(0).LessThanOrEqualTo(5).WithMessage("Score must be between 1 and 5");
    }
}