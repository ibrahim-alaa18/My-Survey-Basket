
using Hangfire;
using Mapster;
using MySurveyBasket.Contracts.Polls;
using MySurveyBasket.Entities;
using MySurveyBasket.Errors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MySurveyBasket.Services
{
    public class PollService(ApplicationDbContext context,INotificationService notificationService) : IPollService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly INotificationService _notificationService = notificationService;

        public async Task<Result<IEnumerable<PollResponse>>> GetAllAsync(CancellationToken cancellationToken=default)
        {
            var polls= await _context.Polls.AsNoTracking().ProjectToType<PollResponse>().ToListAsync(cancellationToken);
            return (polls.Count == 0) ? Result.Failure<IEnumerable<PollResponse>>(PollErrors.NoPollsFound)
                :Result.Success(polls.AsEnumerable());
        }
        public async Task<Result<IEnumerable<PollResponse>>> GetCurrentAsync(CancellationToken cancellationToken = default)
        {
            var polls = await _context.Polls.Where(p=>p.IsPublished==1 && p.StartAt<=DateTime.UtcNow && p.EndAt>=DateTime.UtcNow)
                .AsNoTracking().ProjectToType<PollResponse>().ToListAsync(cancellationToken);
            return (polls.Count == 0) ? Result.Failure<IEnumerable<PollResponse>>(PollErrors.NoPollsFound)
                : Result.Success(polls.AsEnumerable());
        }


        public async Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken=default)
        {
            var poll= await  _context.Polls.FindAsync(id,cancellationToken);
            return poll is null ? Result.Failure<PollResponse>(PollErrors.NoPollFound)
               : Result.Success(poll.Adapt<PollResponse>());

        }

        public async Task<Result<PollResponse>> AddAsync(PollRequest pollRequest, CancellationToken cancellationToken= default)
        {
            var isDuplicate = await _context.Polls.AnyAsync(p => p.Title == pollRequest.Title, cancellationToken);
            if (isDuplicate) return Result.Failure<PollResponse>(PollErrors.DuplicatedPollTitle);

            var poll = pollRequest.Adapt<Poll>();
            await _context.Polls.AddAsync(poll,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(poll.Adapt<PollResponse>());

        }

        public async Task<Result> UpdateAsync(int id, PollRequest pollRequest,CancellationToken cancellationToken)
        {
            var currentpoll = await _context.Polls.FindAsync(id, cancellationToken);
            if (currentpoll is null) return Result.Failure(PollErrors.NoPollFound);

            var isDuplicate = await _context.Polls.AnyAsync(p => p.Title == pollRequest.Title && p.Id!=id, cancellationToken);
            if (isDuplicate) return Result.Failure(PollErrors.DuplicatedPollTitle);

            
            currentpoll.Title = pollRequest.Title!;
            currentpoll.Summary = pollRequest.Summary!;
            currentpoll.StartAt = pollRequest.StartAt;
            currentpoll.EndAt = pollRequest.EndAt;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id,CancellationToken cancellationToken)
        {
            var currentpoll = await _context.Polls.FindAsync(id, cancellationToken);
            if (currentpoll is null)
                return Result.Failure(PollErrors.NoPollFound);
            _context.Remove(currentpoll);
           await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> TogglePublishStatusAsync(int id, CancellationToken cancellationToken)
        {
            var currentpoll = await _context.Polls.FindAsync(id, cancellationToken); 
            if (currentpoll is null) return Result.Failure(PollErrors.NoPollFound);
            currentpoll.IsPublished = currentpoll.IsPublished == 0 ? 1 : 0;
            await _context.SaveChangesAsync(cancellationToken);

            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);
            if (currentpoll.IsPublished == 1 && (currentpoll.StartAt.Date >= tomorrow && currentpoll.EndAt.Date < today))
            {
              BackgroundJob.Enqueue(() => _notificationService.SendNewPollsNotification(currentpoll.Id));
            }
            return Result.Success();
        }
    }
}
