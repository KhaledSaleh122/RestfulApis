using Microsoft.AspNetCore.Mvc;
using RestfulApis_Application.User;

namespace RestfulApis_Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserHandlers _userHandlers;

        public AuthenticationController(UserHandlers userHandlers)
        {
            _userHandlers = userHandlers ?? throw new ArgumentNullException(nameof(userHandlers));
        }

        [HttpPost("/login")]
        public async Task<IActionResult> SignIn(UserDto user)
        {
            var result = await _userHandlers.SignInHandler(user);
            if (!result.IsSuccess)
            {
                return StatusCode(result.ErrorResult!.StatusCode, result.ErrorResult);
            }
            return Ok(result.Value);
        }
    }
}
