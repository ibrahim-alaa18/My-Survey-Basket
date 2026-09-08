
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MySurveyBasket.Entities;
using SurveyBasket.Helpers;

namespace MySurveyBasket.Services
{
    public class NotificationService(
        ApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor,
        IEmailSender emailSender,
        UserManager<ApplicationUser> userManager
        ) : INotificationService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task SendNewPollsNotification(int? pollId = null)
        {
            IEnumerable<Poll> polls = [];

            if (pollId.HasValue)
            {
                var poll = await _context.Polls.SingleOrDefaultAsync(x => x.Id == pollId && x.IsPublished==1);
                if (poll != null)
                {
                    polls = [poll];
                }
            }
            else 
            {
                var today = DateTime.UtcNow.Date;
                var tomorrow = today.AddDays(1);
                polls = await _context.Polls
                    .Where(x => x.IsPublished == 1 && x.StartAt >= today && x.StartAt < tomorrow)
                    .AsNoTracking()
                    .ToListAsync();

            }
            //ToDo: select members only 
            var users = await _userManager.Users
                .AsNoTracking()
                .ToListAsync();

            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            foreach (var poll in polls)
            {
                
                foreach (var user in users)
                {
                    var placeholders = new Dictionary<string, string>
                    {
                        { "{{name}}", user.FirstName! },
                        { "{{pollTill}}", poll.Title },
                        { "{{endDate}}", poll.EndAt.ToString() },
                        { "{{url}}", $"{origin}/polls/start/{poll.Id}" }
                    };

                    var body = EmailBodyBuilder.GenerateEmailBody("PollNotification", placeholders);

                    await _emailSender.SendEmailAsync(user.Email!, $"📣 Survey Basket: New Poll - {poll.Title}", body);
                }
            }
        }
    }
}
