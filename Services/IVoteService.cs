using MySurveyBasket.Contracts.Vote;

namespace MySurveyBasket.Services
{
    public interface IVoteService
    {
        Task<Result> AddAsync(int PollId,VoteRequest request,string UserId, CancellationToken cancellationToken);
    }
}
