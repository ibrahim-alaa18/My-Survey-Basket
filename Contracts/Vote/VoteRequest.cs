namespace MySurveyBasket.Contracts.Vote
{
    public record VoteRequest(
     IEnumerable<VoteAnswerRequest> Answers
    );
}
