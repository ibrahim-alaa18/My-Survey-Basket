namespace MySurveyBasket.Contracts.Results
{
    public record VotesPerDayResponse(
        DateOnly Date,
        int VoteCount
    );

}
