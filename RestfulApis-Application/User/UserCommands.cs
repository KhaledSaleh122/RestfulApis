using Restfulapis_Domain.Abstractions;

namespace RestfulApis_Application.User
{
    public class UserCommands
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public UserCommands(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        public async Task<Result<string>> SignInHandler(UserDto userDto)
        {
            var user = await _userRepository.FindByNameAsync(userDto.Username);
            if (user is null)
            {
                var errors = new List<Error> {
                    new () { Name="UserName" ,Message = "Username doesn’t exist."}
                };
                return new Result<string>(
                    new ErrorResult(401, errors)
                );
            }
            var isPasswordCorrect = await _userRepository.CheckPasswordSignInAsync(user, userDto.Password);
            if (!isPasswordCorrect)
            {
                var errors = new List<Error> {
                    new(){ Name="Password", Message = "Incorrect password."}
                };
                return new Result<string>(
                    new ErrorResult(401, errors)
                );
            }
            return new Result<string>(_tokenService.GenerateUserToken(user));
        }
    }
}
