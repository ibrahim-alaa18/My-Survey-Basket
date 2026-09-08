using FluentValidation;

namespace MySurveyBasket.Contracts.Polls
{
    public class PollRequestValidator: AbstractValidator<PollRequest>
    {
        public PollRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
                
            RuleFor(x => x.Summary)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.StartAt)
                .NotEmpty().WithMessage("Start date is required.")
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Start date cannot be in the past.");

            RuleFor(x => x.EndAt)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThan(x => x.StartAt)
                .WithMessage("End date must be after the start date.");
        }
    }
}
