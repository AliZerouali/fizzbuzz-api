using FluentAssertions;
using FizzBuzz.Application.Services;
using FizzBuzz.Domain.Entities;

namespace FizzBuzz.Application.UnitTests.Services
{
    public class FizzBuzzAppServiceTests
    {
        private readonly FizzBuzzAppService _service;

        public FizzBuzzAppServiceTests()
        {
            _service = new FizzBuzzAppService();
        }

        [Fact]
        public async Task GenerateFizzBuzzAsync_WithValidParameters_ReturnsEntity()
        {
            // Arrange
            var int1 = 3;
            var int2 = 5;
            var limit = 100;
            var str1 = "fizz";
            var str2 = "buzz";

            // Act
            var result = await _service.GenerateFizzBuzzAsync(int1, int2, limit, str1, str2);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FizzBuzzEntity>();
            result.Int1.Should().Be(int1);
            result.Int2.Should().Be(int2);
            result.Limit.Should().Be(limit);
            result.Str1.Should().Be(str1);
            result.Str2.Should().Be(str2);
        }
    }
}