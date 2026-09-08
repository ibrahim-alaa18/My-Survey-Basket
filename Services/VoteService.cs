using Microsoft.EntityFrameworkCore;
using MySurveyBasket.Contracts.Question;
using MySurveyBasket.Contracts.Vote;
using MySurveyBasket.Errors;

namespace MySurveyBasket.Services
{
    public class VoteService(ApplicationDbContext context) : IVoteService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result> AddAsync(int PollId, VoteRequest request, string UserId, CancellationToken cancellationToken)
        {
            var hasvote = await _context.Votes.AnyAsync(v => v.PollId == PollId && v.UserId == UserId, cancellationToken);
            if (hasvote)
                return Result.Failure(VoteErrors.DuplicatedVote);

            var pollIsExists = await _context.Polls.AnyAsync(p => p.Id == PollId && p.IsPublished == 1 && p.StartAt <= DateTime.UtcNow && p.EndAt >= DateTime.UtcNow, cancellationToken);
            if (!pollIsExists)
                return Result.Failure(PollErrors.NoPollFound);

            var availableQuestionIds = await _context.Questions
                .Where(q => q.PollId == PollId && q.IsActive).Select(q => q.Id).ToListAsync(cancellationToken);

            if(!request.Answers.Select(a=>a.QuestionId).SequenceEqual(availableQuestionIds))
                return Result.Failure(VoteErrors.InvalidQuestions);

            var vote = new Vote
            {
                PollId = PollId,
                UserId = UserId,
                VoteAnswers = request.Answers.Adapt<IEnumerable<VoteAnswer>>().ToList()

            };

            await _context.Votes.AddAsync(vote, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
