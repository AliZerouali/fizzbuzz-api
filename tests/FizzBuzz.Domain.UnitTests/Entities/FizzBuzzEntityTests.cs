using FluentAssertions;
using FizzBuzz.Domain.Entities;

namespace FizzBuzz.Domain.UnitTests.Entities
{
    public class FizzBuzzEntityTests
    {
        [Theory]
        [InlineData(3, 5, 100, "fizz", "buzz")]
        [InlineData(2, 7, 50, "foo", "bar")]
        [InlineData(1, 1, 10, "a", "b")]
        public void Create_WithValidParameters_ReturnsEntity(int int1, int int2, int limit, string str1, string str2)
        {
            // Act
            var entity = FizzBuzzEntity.Create(int1, int2, limit, str1, str2);

            // Assert
            entity.Should().NotBeNull();
            entity.Int1.Should().Be(int1);
            entity.Int2.Should().Be(int2);
            entity.Limit.Should().Be(limit);
            entity.Str1.Should().Be(str1);
            entity.Str2.Should().Be(str2);
            entity.Result.Should().HaveCount(limit);
        }

        [Theory]
        [InlineData(0, 5, 100, "fizz", "buzz")]
        [InlineData(-1, 5, 100, "fizz", "buzz")]
        [InlineData(3, 0, 100, "fizz", "buzz")]
        [InlineData(3, 5, 0, "fizz", "buzz")]
        [InlineData(3, 5, -10, "fizz", "buzz")]
        public void Create_WithInvalidNumbers_ThrowsDomainException(int int1, int int2, int limit, string str1, string str2)
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => 
                FizzBuzzEntity.Create(int1, int2, limit, str1, str2));
        }

        [Theory]
        [InlineData(3, 5, 100, "", "buzz")]
        [InlineData(3, 5, 100, "fizz", "")]
        [InlineData(3, 5, 100, null, "buzz")]
        [InlineData(3, 5, 100, "fizz", null)]
        [InlineData(3, 5, 100, "   ", "buzz")]
        public void Create_WithInvalidStrings_ThrowsDomainException(int int1, int int2, int limit, string str1, string str2)
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => 
                FizzBuzzEntity.Create(int1, int2, limit, str1, str2));
        }

        [Fact]
        public void GenerateResult_WithTypicalFizzBuzz_ReturnsCorrectSequence()
        {
            // Arrange
            var entity = FizzBuzzEntity.Create(3, 5, 15, "fizz", "buzz");

            // Act
            var result = entity.Result;

            // Assert
            result.Should().Equal(
                "1", "2", "fizz", "4", "buzz",
                "fizz", "7", "8", "fizz", "buzz",
                "11", "fizz", "13", "14", "fizzbuzz"
            );
        }

        [Fact]
        public void GetCacheKey_ReturnsNormalizedKey()
        {
            // Arrange
            var entity = FizzBuzzEntity.Create(3, 5, 100, "Fizz", "Buzz");

            // Act
            var cacheKey = entity.GetCacheKey();

            // Assert
            cacheKey.Should().Be("3|5|100|fizz|buzz"); // Lowercase pour les strings
        }

        [Theory]
        [InlineData(3, 5, 10, "FIZZ", "BUZZ", "3|5|10|fizz|buzz")]
        [InlineData(2, 4, 8, "Foo", "Bar", "2|4|8|foo|bar")]
        public void GetCacheKey_NormalizesStringCase(int int1, int int2, int limit, string str1, string str2, string expectedKey)
        {
            // Arrange
            var entity = FizzBuzzEntity.Create(int1, int2, limit, str1, str2);

            // Act
            var cacheKey = entity.GetCacheKey();

            // Assert
            cacheKey.Should().Be(expectedKey);
        }
    }
}