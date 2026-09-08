using MySurveyBasket.Contracts.Results;

namespace MySurveyBasket.Services
{
    public interface IResultService
    {
        Task<Result<PollVotesResponse>> GetPollVotesAsync(int PollId, CancellationToken cancellationToken);
        Task<Result<IEnumerable<VotesPerDayResponse>>> GetVotesPerDayAsync(int PollId, CancellationToken cancellationToken);

        Task<Result<IEnumerable<VotesPerQuestionResponse>>> GetVotesPerQuestionAsync(int PollId, CancellationToken cancellationToken);
    }
}
