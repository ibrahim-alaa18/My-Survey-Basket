using MySurveyBasket.Contracts.Question;
using MySurveyBasket.Contracts.Results;
using MySurveyBasket.Errors;
using System.Collections.Generic;

namespace MySurveyBasket.Services
{
    public class ResultService(ApplicationDbContext context) : IResultService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<PollVotesResponse>> GetPollVotesAsync(int PollId, CancellationToken cancellationToken)
        {
            var pollVotes = await _context.Polls
                .Where(p => p.Id == PollId)
                .Select(p => new PollVotesResponse(
                   p.Title,
                   p.Votes.Select(v => new VoteResponse(
                         $"{v.User.FirstName} {v.User.LastName}",
                         v.SubmittedOn,
                         v.VoteAnswers.Select(va => new QuestionAnswerResponse(
                             va.Question.Content,
                             va.Answer.Content

                         ))


                       ))


                )).SingleOrDefaultAsync(cancellationToken);

            return pollVotes is null
                ? Result.Failure<PollVotesResponse>(PollErrors.NoPollFound)
                : Result.Success<PollVotesResponse>(pollVotes);

        }

        public async Task<Result<IEnumerable<VotesPerDayResponse>>> GetVotesPerDayAsync(int PollId, CancellationToken cancellationToken)
        {
            var pollIsExists = await _context.Polls.AnyAsync(p => p.Id == PollId && p.IsPublished == 1 && p.StartAt <= DateTime.UtcNow && p.EndAt >= DateTime.UtcNow, cancellationToken);
            if (!pollIsExists)
                return Result.Failure<IEnumerable<VotesPerDayResponse>>(PollErrors.NoPollFound);

            var votesPerDay = await _context.Votes
                .Where(v => v.PollId == PollId)
                .GroupBy(v => new { Date = DateOnly.FromDateTime(v.SubmittedOn) })
                .Select(g => new VotesPerDayResponse
                (
                   g.Key.Date,
                   g.Count()
                ))
                .ToListAsync(cancellationToken);
            return votesPerDay is null
                ? Result.Failure<IEnumerable<VotesPerDayResponse>>(VoteErrors.NoVotesFound)
                : Result.Success<IEnumerable<VotesPerDayResponse>>(votesPerDay);
        }

        public async Task<Result<IEnumerable<VotesPerQuestionResponse>>> GetVotesPerQuestionAsync(int PollId, CancellationToken cancellationToken)
        {
            var pollIsExists = await _context.Polls.AnyAsync(p => p.Id == PollId && p.IsPublished == 1 && p.StartAt <= DateTime.UtcNow && p.EndAt >= DateTime.UtcNow, cancellationToken);
            if (!pollIsExists)
                return Result.Failure<IEnumerable<VotesPerQuestionResponse>>(PollErrors.NoPollFound);

            var votesPerQuestion = await _context.VoteAnswers
                .Where(va => va.Vote.PollId == PollId)
                .Select(va=> new VotesPerQuestionResponse(
                        va.Question.Content,
                        va.Question.VoteAnswers
                          .GroupBy(va => new { AnswerId = va.AnswerId, Answercontent = va.Answer.Content })
                           .Select(g => new VotesPerAnswerResponse(
                                g.Key.Answercontent,
                                g.Count()
                            ))

                  ))
                .ToListAsync(cancellationToken);
            
            if (votesPerQuestion is null || votesPerQuestion.Count == 0)
                return Result.Failure<IEnumerable<VotesPerQuestionResponse>>(VoteErrors.NoVotesFound);

            else
                return Result.Success<IEnumerable<VotesPerQuestionResponse>>(votesPerQuestion);





        }


    }
}
