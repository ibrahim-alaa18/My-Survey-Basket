namespace MySurveyBasket.Contracts.Question
{
    public class QuestionRequestValidator:AbstractValidator<QuestionRequest>
    {
        public QuestionRequestValidator()
        {
            RuleFor(q => q.Content)
                   .NotEmpty()
                   .Length(3, 1000);

            RuleFor(q => q.Answers)
                   .NotNull();

            RuleFor(q => q.Answers)
                   .Must(a => a.Count > 1)
                   .WithMessage("Question Should Has At Least 2 Answers")
                   .When(a=>a.Answers !=null);

            RuleFor(q => q.Answers)
                    .Must(a => a.Distinct().Count() == a.Count())
                    .WithMessage("You Cannot Add Duplicated Answers")
                    .When(a => a.Answers != null); 
                
        }
    }
}
