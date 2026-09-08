namespace MySurveyBasket.Contracts.Results
{
    public record VotesPerAnswerResponse(   
        string Answer,
        int Count
    );
}
