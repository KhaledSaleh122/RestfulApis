using Microsoft.AspNetCore.Mvc;
using RestfulApis_Application.User;
using Restfulapis_Domain.Abstractions;

namespace RestfulApis_Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost("/login")]
        public async Task<IActionResult> SignIn(UserDto user) {
            var command = new UserHandlers(_userRepository, _tokenService);
            var result = await command.SignInHandler(user);
            if (!result.IsSuccess) {
                return StatusCode(result.ErrorResult!.StatusCode, result.ErrorResult);
            }
            return Ok(result.Value);
        }
    }
}
