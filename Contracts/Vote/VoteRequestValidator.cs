namespace MySurveyBasket.Contracts.Vote
{
    public class VoteRequestValidator : AbstractValidator<VoteRequest>
    {
        public VoteRequestValidator()
        {
            RuleFor(v => v.Answers)
                .NotEmpty().WithMessage("At least one answer must be provided.");
            RuleForEach(v => v.Answers)
                .SetInheritanceValidator(v=>v.Add(new VoteAnswerRequestValidator()));

        }
    }
}
