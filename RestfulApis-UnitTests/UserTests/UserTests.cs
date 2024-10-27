using AutoFixture;
using FluentAssertions;
using Moq;
using RestfulApis_Application.User;
using Restfulapis_Domain.Abstractions;
using Restfulapis_Domain.Entities;

namespace RestfulApis_UnitTests.UserTests
{
    public class UserTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly IFixture _fixture;
        private readonly UserHandlers _handler;
        public UserTests()
        {
            _userRepositoryMock = new();
            _tokenServiceMock = new();
            _fixture = new Fixture();
            _handler = new UserHandlers(
                _userRepositoryMock.Object,
                _tokenServiceMock.Object
            );
        }

        [Fact]
        public async Task SignInHandler_ShouldReturnError_WhenUsernameDoesNotExist()
        {
            // Arrange
            var userDto = _fixture.Build<UserDto>()
                                  .With(x => x.Username, "nonexistentuser")
                                  .With(x => x.Password, "password123")
                                  .Create();

            _userRepositoryMock.Setup(repo => repo.FindByNameAsync(userDto.Username))
                               .ReturnsAsync((User?)null);

            // Act
            var result = await _handler.SignInHandler(userDto);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorResult!.StatusCode.Should().Be(401);
            result.ErrorResult.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task SignInHandler_ShouldReturnError_WhenPasswordIsIncorrect()
        {
            // Arrange
            var user = _fixture.Create<User>();
            var userDto = _fixture.Build<UserDto>()
                                  .With(x => x.Username, user.Username)
                                  .With(x => x.Password, "wrongpassword")
                                  .Create();

            _userRepositoryMock.Setup(repo => repo.FindByNameAsync(userDto.Username))
                               .ReturnsAsync(user);

            _userRepositoryMock.Setup(repo => repo.CheckPasswordSignInAsync(user, userDto.Password))
                               .ReturnsAsync(false);

            // Act
            var result = await _handler.SignInHandler(userDto);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorResult!.StatusCode.Should().Be(401);
            result.ErrorResult.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task SignInHandler_ShouldReturnSuccess_WhenUsernameAndPasswordAreCorrect()
        {
            // Arrange
            var user = _fixture.Create<User>();
            var userDto = _fixture.Build<UserDto>()
                                  .With(x => x.Username, user.Username)
                                  .With(x => x.Password, "correctpassword")
                                  .Create();

            var token = "generated_jwt_token";

            _userRepositoryMock.Setup(repo => repo.FindByNameAsync(userDto.Username))
                               .ReturnsAsync(user);

            _userRepositoryMock.Setup(repo => repo.CheckPasswordSignInAsync(user, userDto.Password))
                               .ReturnsAsync(true);

            _tokenServiceMock.Setup(service => service.GenerateUserToken(user))
                             .Returns(token);

            // Act
            var result = await _handler.SignInHandler(userDto);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(token);
        }
    }
}
