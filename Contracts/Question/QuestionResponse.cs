using MySurveyBasket.Contracts.Answers;

namespace MySurveyBasket.Contracts.Question
{
    public record QuestionResponse
    (
        int Id,
        string Content,
        IEnumerable<AnswerResponse> Answers

    );
}
