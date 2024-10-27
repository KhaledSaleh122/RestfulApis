using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using RestfulApis_Application.TopicSpace;
using Restfulapis_Domain.Abstractions;
using Restfulapis_Domain.Entities;

namespace RestfulApis_UnitTests.TopicSpace
{
    public class TopicTests
    {
        private readonly Mock<ITopicRepository> _topicRepositoryMock;
        private readonly Mock<IValidator<TopicDto>> _validatorMock;
        private readonly IMapper _mapper;
        private readonly TopicHandlers _handlers;
        private readonly Fixture _fixture;

        public TopicTests()
        {
            _topicRepositoryMock = new Mock<ITopicRepository>();
            _validatorMock = new Mock<IValidator<TopicDto>>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TopicDto, Topic>());
            _mapper = config.CreateMapper();

            _handlers = new TopicHandlers(_topicRepositoryMock.Object, _validatorMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task CreateTopicHandler_ShouldReturnError_WhenValidationFails()
        {
            // Arrange
            var topicDto = _fixture.Create<TopicDto>();

            var validationFailures = _fixture.CreateMany<ValidationFailure>();

            _validatorMock.Setup(v => v.ValidateAsync(topicDto, default))
                          .ReturnsAsync(new ValidationResult(validationFailures));

            // Act
            var result = await _handlers.CreateTopicHandler(topicDto);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorResult.StatusCode.Should().Be(400);
            result.ErrorResult.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateTopicHandler_ShouldReturnSuccess_WhenValidationPasses()
        {
            // Arrange
            var topicDto = _fixture.Create<TopicDto>();

            _validatorMock.Setup(v => v.ValidateAsync(topicDto, default))
                          .ReturnsAsync(new ValidationResult());

            _topicRepositoryMock.Setup(repo => repo.CreateTopicAsync(It.IsAny<Topic>()))
                                .Returns(Task.CompletedTask);

            // Act
            var result = await _handlers.CreateTopicHandler(topicDto);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _topicRepositoryMock.Verify(repo => repo.CreateTopicAsync(It.IsAny<Topic>()), Times.Once);
        }

        [Fact]
        public async Task GetTopicByIdHandler_ShouldReturnError_WhenTopicNotFound()
        {
            // Arrange
            int topicId = 1;
            _topicRepositoryMock.Setup(repo => repo.FindTopicByIdAsync(topicId))
                                .ReturnsAsync((Topic?)null);

            // Act
            var result = await _handlers.GetTopicByIdHandler(topicId);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorResult!.StatusCode.Should().Be(404);
            result.ErrorResult.Errors.Should().NotBeEmpty();
        }
        [Fact]
        public async Task GetTopicByIdHandler_ShouldReturnSuccess_WhenTopicFound()
        {
            // Arrange
            var topicId = 1;
            var topic = _fixture.Create<Topic>();

            _topicRepositoryMock.Setup(repo => repo.FindTopicByIdAsync(topicId))
                                .ReturnsAsync(topic);

            // Act
            var result = await _handlers.GetTopicByIdHandler(topicId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(topic);
        }

        [Fact]
        public async Task GetTopicsHandler_ShouldReturnPagedResult_WhenTopicsExist()
        {
            // Arrange
            int page = 1;
            int pageSize = 10;
            var topics = _fixture.CreateMany<Topic>(pageSize).ToList();
            int totalRecords = 50;

            _topicRepositoryMock.Setup(repo => repo.FindTopics(page, pageSize))
                                .ReturnsAsync((topics, totalRecords));

            // Act
            var result = await _handlers.GetTopicsHandler(page, pageSize);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value!.Page.Should().Be(page);
            result.Value.PageSize.Should().Be(pageSize);
            result.Value.TotalRecords.Should().Be(totalRecords);
            result.Value.Results.Should().BeEquivalentTo(topics);
        }

        [Fact]
        public async Task GetTopicsHandler_ShouldReturnEmptyResult_WhenNoTopicsExist()
        {
            // Arrange
            int page = 1;
            int pageSize = 10;
            var topics = new List<Topic>();
            int totalRecords = 0;

            _topicRepositoryMock.Setup(repo => repo.FindTopics(page, pageSize))
                                .ReturnsAsync((topics, totalRecords));

            // Act
            var result = await _handlers.GetTopicsHandler(page, pageSize);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value!.Page.Should().Be(page);
            result.Value.PageSize.Should().Be(pageSize);
            result.Value.TotalRecords.Should().Be(0);
            result.Value.Results.Should().BeEmpty();
        }
    }
}
