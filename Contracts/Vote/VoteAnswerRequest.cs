namespace MySurveyBasket.Contracts.Vote
{
    public record VoteAnswerRequest(
      int QuestionId,
      int AnswerId
    );
    
}
