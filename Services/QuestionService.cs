using MySurveyBasket.Contracts.Answers;
using MySurveyBasket.Contracts.Question;
using MySurveyBasket.Entities;
using MySurveyBasket.Errors;
using System.Collections.Generic;

namespace MySurveyBasket.Services
{
    public class QuestionService(ApplicationDbContext context,ICasheService casheService,ILogger<QuestionService> logger) : IQuestionService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ICasheService _casheService = casheService;
        private readonly ILogger<QuestionService> _logger = logger;
        private const string _cacheKeyPrefix = "Available Questions";

        public async Task<Result<IEnumerable<QuestionResponse>>> GetAllAsync(int PollId, CancellationToken cancellationToken)
        {
            var pollIsExists = await _context.Polls.AnyAsync(p => p.Id == PollId, cancellationToken);

            if (!pollIsExists)
                return Result.Failure<IEnumerable<QuestionResponse>>(PollErrors.NoPollFound);
            var questions = await _context.Questions
                .Where(q => q.PollId == PollId)
                .Include(q => q.Answers)
                .ProjectToType<QuestionResponse>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return Result.Success<IEnumerable<QuestionResponse>>(questions);

        }


        public async Task<Result<QuestionResponse>> GetAsync(int PollId, int Id, CancellationToken cancellationToken)
        {
            var pollIsExists = await _context.Polls.AnyAsync(p => p.Id == PollId, cancellationToken);

            if (!pollIsExists)
                return Result.Failure<QuestionResponse>(PollErrors.NoPollFound);
           
            var question = await _context.Questions
                 .Where(q => q.PollId == PollId && q.Id==Id)
                 .Include(q => q.Answers)
                 .ProjectToType<QuestionResponse>()
                 .AsNoTracking()
                 .SingleOrDefaultAsync(cancellationToken);

            if (question==null)
                return Result.Failure<QuestionResponse>(QuestionErrors.NoQuestionFound);

            return Result.Success<QuestionResponse>(question.Adapt<QuestionResponse>());



        }

        public async Task<Result<IEnumerable<QuestionResponse>>> GetAvailableAsync(int PollId, string userId, CancellationToken cancellationToken)
        {
            var hasvote =await _context.Votes.AnyAsync(v => v.PollId == PollId && v.UserId == userId, cancellationToken);
            if (hasvote)
                return Result.Failure<IEnumerable<QuestionResponse>>(VoteErrors.DuplicatedVote);

            var pollIsExists = await _context.Polls.AnyAsync(p => p.Id == PollId && p.IsPublished == 1 && p.StartAt <= DateTime.UtcNow && p.EndAt >= DateTime.UtcNow, cancellationToken);
            if (!pollIsExists)
                return Result.Failure<IEnumerable<QuestionResponse>>(PollErrors.NoPollFound);

            var cacheKey = $"{_cacheKeyPrefix}_{PollId}";
            var cashedQuestions= await _casheService.GetAsync<IEnumerable<QuestionResponse>>(cacheKey, cancellationToken);

            IEnumerable<QuestionResponse> questions = [];

            if (cashedQuestions == null)
            {
                _logger.LogInformation("Get Data from Database");
                questions = await _context.Questions
                .Where(q => q.PollId == PollId && q.IsActive)
                .Include(q => q.Answers)
                .Select(q => new QuestionResponse
                (
                   q.Id,
                   q.Content,
                   q.Answers.Where(a => a.IsActive).Select(a => new AnswerResponse
                    (
                         a.Id,
                         a.Content
                    ))
                ))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
                
                await _casheService.SetAsync(cacheKey, questions, TimeSpan.FromMinutes(30), cancellationToken);

            }
            else 
            {
                _logger.LogInformation("Get Data from Cashe");
                questions = cashedQuestions;
            }    

            return Result.Success<IEnumerable<QuestionResponse>>(questions);
        }


        public async Task<Result<QuestionResponse>> AddAsync(int PollId, QuestionRequest request, CancellationToken cancellationToken)
        {
            var pollIsExists= await _context.Polls.AnyAsync(p =>p.Id == PollId,  cancellationToken);
           
            if(!pollIsExists)
                return Result.Failure<QuestionResponse>(PollErrors.NoPollFound);
            var questionIsExists= await _context.Questions.AnyAsync(q => q.Content==request.Content && q.PollId==PollId, cancellationToken);
               if(questionIsExists)
                   return Result.Failure<QuestionResponse>(QuestionErrors.DuplicatedQuestionContent);

            var question= request.Adapt<Question>(); 

            question.PollId = PollId;
            
            await _context.AddAsync(question, cancellationToken);
            await _context.SaveChangesAsync();
            await _casheService.RemoveAsync($"{_cacheKeyPrefix}_{PollId}", cancellationToken);
            return Result.Success(question.Adapt<QuestionResponse>());



        }

       
        public async Task<Result> UpdateAsync(int PollId, int id, QuestionRequest request, CancellationToken cancellationToken)
        {
            var questionIsExists=await _context.Questions
                .AnyAsync(q=>q.PollId==PollId
                && q.Id!=id
                && q.Content==request.Content
                    
            ); 

            if(questionIsExists)
                return Result.Failure(QuestionErrors.DuplicatedQuestionContent);

            var question= await _context.Questions
                .Include(q=>q.Answers)
                .SingleOrDefaultAsync(q=>q.PollId == PollId && q.Id == id,cancellationToken);
            if (question == null)
                return Result.Failure(QuestionErrors.NoQuestionFound);

            question.Content= request.Content;

            //current answers

            var currentAnswers=question.Answers.Select(a=>a.Content).ToList();

            //new answers

            var newAnswers = request.Answers.Except(currentAnswers).ToList();

            newAnswers.ForEach(a =>
                {
                    question.Answers.Add(new Answer { Content = a });
                }
            );

            question.Answers.ToList().ForEach(a =>
                    {
                        a.IsActive = request.Answers.Contains(a.Content);
                    });

            await _context.SaveChangesAsync();
            await _casheService.RemoveAsync($"{_cacheKeyPrefix}_{PollId}", cancellationToken);

            return Result.Success();









        }

        public async Task<Result> ToggleStatusAsync(int PollId, int id, CancellationToken cancellationToken)
        {
            var question = await _context.Questions.SingleOrDefaultAsync(q=>q.PollId==PollId && q.Id==id ,cancellationToken);

            if (question is null) return Result.Failure(QuestionErrors.NoQuestionFound);

            question.IsActive = !question.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
            await _casheService.RemoveAsync($"{_cacheKeyPrefix}_{PollId}", cancellationToken);

            return Result.Success();
        }

      
    }
}
