
using MySurveyBasket.Contracts.Polls;

namespace MySurveyBasket.Services
{
    public interface IPollService
    {
        Task<Result<IEnumerable<PollResponse>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<IEnumerable<PollResponse>>> GetCurrentAsync(CancellationToken cancellationToken);

        Task<Result<PollResponse>> GetAsync(int id,CancellationToken cancellationToken);
        Task<Result<PollResponse>> AddAsync(PollRequest pollRequest,CancellationToken cancellationToken);
        Task<Result> UpdateAsync(int id, PollRequest poll,CancellationToken cancellationToken);
        Task<Result> DeleteAsync(int id,CancellationToken cancellationToken);
        Task<Result> TogglePublishStatusAsync(int id,CancellationToken cancellationToken);

    }
}
