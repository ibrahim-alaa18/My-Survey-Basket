using MySurveyBasket.Abstractions;

namespace MySurveyBasket.Errors
{
    public class PollErrors
    {
        public static readonly Error NoPollsFound = new("Poll.NoPollsFound", "No Polls Found",StatusCodes.Status404NotFound);

        public static readonly Error NoPollFound = new("Poll.NoPollFound", "No Poll Found with given Id",StatusCodes.Status404NotFound);

        public static readonly Error DuplicatedPollTitle = new("Poll.DuplicatedTitle", "Another poll with the same title already exist",StatusCodes.Status409Conflict);


    }
}
