using AutoFixture;
using FluentAssertions;
using Moq;
using RestfulApis_Application.CurrencySpace;
using Restfulapis_Domain.Abstractions;
using Restfulapis_Domain.Entities;

namespace RestfulApis_UnitTests.CurrencySpace
{
    public class CurrencyTests
    {
        private readonly Mock<ICurrencyRepository> _currencyRepositoryMock;
        private readonly CurrencyHandlers _handlers;
        private readonly Fixture _fixture;

        public CurrencyTests()
        {
            _currencyRepositoryMock = new Mock<ICurrencyRepository>();
            _handlers = new CurrencyHandlers(_currencyRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetLatestCurrencyExchange_ShouldReturnError_WhenCurrencyIsNull()
        {
            // Arrange
            _currencyRepositoryMock.Setup(repo => repo.GetLatestCurrencyExchange())
                                   .ReturnsAsync((Currency?)null);

            // Act
            var result = await _handlers.GetLatestCurrencyExchange();

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorResult!.StatusCode.Should().Be(502);
            result.ErrorResult.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetLatestCurrencyExchange_ShouldReturnSuccess_WhenCurrencyIsNotNull()
        {
            // Arrange
            var currency = _fixture.Create<Currency>();

            _currencyRepositoryMock.Setup(repo => repo.GetLatestCurrencyExchange())
                                   .ReturnsAsync(currency);

            // Act
            var result = await _handlers.GetLatestCurrencyExchange();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(currency);
        }
    }
}
