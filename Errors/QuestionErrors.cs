using MySurveyBasket.Abstractions;

namespace MySurveyBasket.Errors
{
    public class QuestionErrors
    {
        public static readonly Error NoQuestionsFound = new("Question.NoPollsFound", "No Questions Found", StatusCodes.Status404NotFound);

        public static readonly Error NoQuestionFound = new("Question.NoQuestionFound", "No Question Found with given Id", StatusCodes.Status404NotFound);

        public static readonly Error DuplicatedQuestionContent = new("Question.DuplicatedContent", "Another Question with the same content already exist", StatusCodes.Status409Conflict);


    }
}
