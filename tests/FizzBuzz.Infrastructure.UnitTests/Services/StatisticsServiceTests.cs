using FluentAssertions;
using FizzBuzz.Domain.Entities;
using FizzBuzz.Infrastructure.Persistence;
using FizzBuzz.Infrastructure.Services;
using Moq;

namespace FizzBuzz.Infrastructure.UnitTests.Services
{
    public class StatisticsServiceTests
    {
        private readonly Mock<IStatisticsRepository> _repositoryMock;
        private readonly StatisticsService _service;

        public StatisticsServiceTests()
        {
            _repositoryMock = new Mock<IStatisticsRepository>();
            _service = new StatisticsService(_repositoryMock.Object);
        }

        [Fact]
        public async Task RecordRequestAsync_ValidEntity_CallsRepository()
        {
            // Arrange
            var entity = FizzBuzzEntity.Create(3, 5, 100, "fizz", "buzz");
            var expectedKey = "3|5|100|fizz|buzz";

            _repositoryMock.Setup(r => r.IncrementRequestCountAsync(It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            await _service.RecordRequestAsync(entity);

            // Assert
            _repositoryMock.Verify(r => r.IncrementRequestCountAsync(expectedKey), Times.Once);
        }

        [Fact]
        public async Task GetMostFrequentRequestAsync_NoRequests_ReturnsEmptyStatistics()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetMostFrequentRequestKeyAsync())
                .ReturnsAsync((string.Empty, 0));

            // Act
            var result = await _service.GetMostFrequentRequestAsync();

            // Assert
            result.Should().NotBeNull();
            result.MostFrequentRequest.Should().BeNull();
            result.HitCount.Should().Be(0);
            result.LastUpdated.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public async Task GetMostFrequentRequestAsync_WithRequests_ReturnsMostFrequent()
        {
            // Arrange
            var expectedKey = "3|5|100|fizz|buzz";
            var expectedCount = 5;

            _repositoryMock.Setup(r => r.GetMostFrequentRequestKeyAsync())
                .ReturnsAsync((expectedKey, expectedCount));

            // Act
            var result = await _service.GetMostFrequentRequestAsync();

            // Assert
            result.Should().NotBeNull();
            result.MostFrequentRequest.Should().NotBeNull();
            result.MostFrequentRequest!.Int1.Should().Be(3);
            result.MostFrequentRequest!.Int2.Should().Be(5);
            result.MostFrequentRequest!.Limit.Should().Be(100);
            result.MostFrequentRequest!.Str1.Should().Be("fizz");
            result.MostFrequentRequest!.Str2.Should().Be("buzz");
            result.HitCount.Should().Be(expectedCount);
        }
    }
}