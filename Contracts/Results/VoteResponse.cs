namespace MySurveyBasket.Contracts.Results
{
    public record VoteResponse(
        string VoterName,
        DateTime VotedAt,
        IEnumerable<QuestionAnswerResponse> SelectedAnswers
    );
    
}
