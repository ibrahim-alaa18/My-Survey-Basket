using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySurveyBasket.Contracts.Vote;
using SurveyBasket.Abstractions.Consts;
using System.Security.Claims;

namespace MySurveyBasket.Controllers
{
    [Route("api/Polls/{PollId}/Vote")]
    [ApiController]
    [Authorize(Roles = DefaultRoles.Member)]
    public class VotesController(IQuestionService questionService,IVoteService voteService) : ControllerBase
    {
        private readonly IQuestionService _questionService = questionService;
        private readonly IVoteService _voteService = voteService;

        [HttpGet]
        public async Task<IActionResult> GetAvailableQuestions([FromRoute] int PollId, CancellationToken cancellationToken)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _questionService.GetAvailableAsync(PollId, UserId!, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) :
                   result.ToProblem();

        }

        [HttpPost]
        public async Task<IActionResult> Vote([FromRoute] int PollId,[FromBody] VoteRequest request, CancellationToken cancellationToken)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _voteService.AddAsync(PollId,request, UserId!, cancellationToken);
            return  result.IsSuccess? Created()
                : result.ToProblem();



        }


    }
}
