using MySurveyBasket.Contracts.Question;

namespace MySurveyBasket.Services
{
    public interface IQuestionService
    {
        Task<Result<IEnumerable<QuestionResponse>>> GetAllAsync(int PollId, CancellationToken cancellationToken);
        Task<Result<QuestionResponse>> GetAsync(int PollId,int Id, CancellationToken cancellationToken);
        Task<Result<IEnumerable<QuestionResponse>>> GetAvailableAsync(int PollId, string userId, CancellationToken cancellationToken);

        Task<Result<QuestionResponse>> AddAsync(int PollId,QuestionRequest request,CancellationToken cancellationToken);
        Task<Result> UpdateAsync(int PollId,int id, QuestionRequest request, CancellationToken cancellationToken);

        Task<Result> ToggleStatusAsync(int pollId ,int id, CancellationToken cancellationToken);

    }

}
