using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Contracts.Users;
using System.Security.Claims;

namespace MySurveyBasket.Controllers
{
    [Route("/myprofile")]
    [ApiController]
    [Authorize]
    public class AccountController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("")]
        public async Task<IActionResult> Info()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _userService.GetProfileAsync(userId!);

            return Ok(result.Value);
        }

        [HttpPut("update-info")]
        public async Task<IActionResult> Info([FromBody]UpdateProfileRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _userService.UpdateProfileAsync(userId!,request);

            return NoContent();
        }


        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _userService.ChangePasswordAsync(userId!, request);

            return result.IsSuccess? NoContent():result.ToProblem();
        }
    }
}
