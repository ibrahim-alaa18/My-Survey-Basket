using MySurveyBasket.Abstractions;

namespace MySurveyBasket.Errors
{
    public class VoteErrors
    {
        public static readonly Error DuplicatedVote= new("Vote.DuplicatedVote", "The user has Already voted for this Poll", StatusCodes.Status409Conflict);

        public static readonly Error InvalidQuestions = new("Vote.InvalidQuestions", "InvalidQuestions", StatusCodes.Status400BadRequest);
       
        public static readonly Error NoVotesFound = new("Vote.NoVotesFound", "NoVotesFound", StatusCodes.Status404NotFound);


    }
}
