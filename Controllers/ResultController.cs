using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Abstractions.Consts;
using SurveyBasket.Authentication.Filters;

namespace MySurveyBasket.Controllers
{
    [Route("api/Polls/{pollId}/[controller]")]
    [ApiController]
    [HasPermission(Permissions.Results)]
    public class ResultController(IResultService resultService) : ControllerBase
    {
        private readonly IResultService _resultService= resultService;

        [HttpGet("row-data")]
        public async Task<IActionResult> PollVotes([FromRoute] int pollId, CancellationToken cancellationToken)
        {
           var result = await _resultService.GetPollVotesAsync(pollId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value)
                : result.ToProblem();
        }

        [HttpGet("Votes-Per-Date")]
        public async Task<IActionResult> VotesPerDate([FromRoute] int pollId, CancellationToken cancellationToken)
        {
            var result = await _resultService.GetVotesPerDayAsync(pollId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value)
                : result.ToProblem();
        }

        [HttpGet("Votes-Per-Question")]
        public async Task<IActionResult> VotesPerQuestion([FromRoute] int pollId, CancellationToken cancellationToken)
        {
            var result = await _resultService.GetVotesPerQuestionAsync(pollId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value)
                : result.ToProblem();
        }

    }
}
