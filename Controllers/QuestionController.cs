using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySurveyBasket.Contracts.Question;
using SurveyBasket.Abstractions.Consts;
using SurveyBasket.Authentication.Filters;

namespace MySurveyBasket.Controllers
{
    [Route("api/Polls/{pollId}/[controller]")]
    [ApiController]
    public class QuestionController(IQuestionService questionService) : ControllerBase
    {
        private readonly IQuestionService _questionService = questionService;

        [HttpGet("")]
        [HasPermission(Permissions.GetQuestions)]
        public async Task<IActionResult> GetAll([FromRoute] int pollId, CancellationToken cancellationToken)
        {
            var result=await _questionService.GetAllAsync(pollId, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }




        [HttpGet("{id}")]
        [HasPermission(Permissions.GetQuestions)]
        public async Task<IActionResult> Get([FromRoute] int pollId,[FromRoute]int id, CancellationToken cancellationToken)
        {
            var result = await _questionService.GetAsync(pollId,id, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("")]
        [HasPermission(Permissions.AddQuestions)]
        public async Task<IActionResult> Add([FromRoute] int pollId, [FromBody] QuestionRequest request,CancellationToken cancellationToken)
        {
            var result= await _questionService.AddAsync(pollId, request, cancellationToken);
            return result.IsSuccess ? CreatedAtAction(nameof(Get), new {pollId,result.Value.Id},result.Value)
                   : result.ToProblem();
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.UpdateQuestions)]
        public async Task<IActionResult> update([FromRoute] int pollId,[FromRoute] int id, [FromBody] QuestionRequest request, CancellationToken cancellationToken)
        {
            var isUpdated = await _questionService.UpdateAsync(pollId,id, request, cancellationToken);
            return isUpdated.IsSuccess ? NoContent()
                   : isUpdated.ToProblem();
        }

        [HttpPut("{id}/ToggleStatus")]
        [HasPermission(Permissions.UpdateQuestions)]
        public async Task<IActionResult> ToggleStatus([FromRoute] int pollId, [FromRoute] int id, CancellationToken cancellationToken)
        {
            var isupdated = await _questionService.ToggleStatusAsync(pollId,id, cancellationToken);
            return isupdated.IsSuccess ? NoContent()
                : isupdated.ToProblem();

        }

    }
}
