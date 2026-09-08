
using Mapster;
using Microsoft.AspNetCore.Authorization;
using MySurveyBasket.Contracts.Polls;
using MySurveyBasket.Entities;
using SurveyBasket.Abstractions.Consts;
using SurveyBasket.Authentication.Filters;

namespace MySurveyBasket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController(IPollService pollService) : ControllerBase
    {
        private readonly IPollService _pollService = pollService;

        [HttpGet]
        [HasPermission(Permissions.GetPolls)]
        public async Task<IActionResult> GetAllASync(CancellationToken cancellationToken=default)
        {
            var polls =  await _pollService.GetAllAsync(cancellationToken);
            return polls.IsSuccess? Ok(polls.Value)
                : polls.ToProblem();
        }

        [HttpGet("Current")]
        [Authorize(Roles = DefaultRoles.Member)]
        public async Task<IActionResult> GetCurrent(CancellationToken cancellationToken = default)
        {
            var polls = await _pollService.GetCurrentAsync(cancellationToken);
            return polls.IsSuccess ? Ok(polls.Value)
                : polls.ToProblem();
        }



        [HttpGet("{id}", Name = "GetPollById")]
        [HasPermission(Permissions.GetPolls)]
        public async Task<IActionResult> Get([FromRoute] int id,CancellationToken cancellationToken)
        {
            var poll = await _pollService.GetAsync(id, cancellationToken);
            return poll.IsSuccess ? Ok(poll.Value)
                :poll.ToProblem();
        }


        [HttpPost]
        [HasPermission(Permissions.AddPolls)]

        public async Task<IActionResult> Add([FromBody] PollRequest pollRequest, CancellationToken cancellationToken=default)
        {
           
            var result =  await _pollService.AddAsync(pollRequest, cancellationToken);
            return result.IsSuccess ?
                CreatedAtRoute("GetPollById", new { id = result.Value.Id }, result.Value)
                : result.ToProblem();


        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.UpdatePolls)]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PollRequest pollRequest,CancellationToken cancellationToken)
        {
            var isupdated = await _pollService.UpdateAsync(id, pollRequest,cancellationToken);
           return isupdated.IsSuccess ? NoContent()
                : isupdated.ToProblem();
        
        }
        [HttpDelete("{id}")]
        [HasPermission(Permissions.DeletePolls)]

        public async Task<IActionResult> Delete([FromRoute] int id,CancellationToken cancellationToken)
        {
            var isdeleted =  await _pollService.DeleteAsync(id, cancellationToken);
            return isdeleted.IsSuccess ? NoContent()
                : isdeleted.ToProblem();

        }

        [HttpPut("{id}/TogglePublish")]
        [HasPermission(Permissions.UpdatePolls)]

        public async Task<IActionResult> TogglePublish([FromRoute] int id, CancellationToken cancellationToken)
        {
            var isupdated = await _pollService.TogglePublishStatusAsync(id,  cancellationToken);
            return isupdated.IsSuccess ? NoContent()
                : isupdated.ToProblem();

        }

    }
}
