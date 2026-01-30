using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;
using FizzBuzz.WebApi.Controllers;
using FizzBuzz.Application.Dtos;
using FizzBuzz.Application.Interfaces;
using FizzBuzz.Domain.Entities;

namespace FizzBuzz.WebApi.UnitTests.Controllers
{
    public class FizzBuzzControllerTests
    {
        private readonly Mock<IFizzBuzzService> _fizzBuzzServiceMock;
        private readonly Mock<IStatisticsService> _statisticsServiceMock;
        private readonly Mock<ILogger<FizzBuzzController>> _loggerMock;
        private readonly FizzBuzzController _controller;

        public FizzBuzzControllerTests()
        {
            _fizzBuzzServiceMock = new Mock<IFizzBuzzService>();
            _statisticsServiceMock = new Mock<IStatisticsService>();
            _loggerMock = new Mock<ILogger<FizzBuzzController>>();
            _controller = new FizzBuzzController(
                _fizzBuzzServiceMock.Object,
                _statisticsServiceMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task Generate_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var requestDto = new FizzBuzzRequestDto
            {
                Int1 = 3,
                Int2 = 5,
                Limit = 100,
                Str1 = "fizz",
                Str2 = "buzz"
            };

            var entity = FizzBuzzEntity.Create(3, 5, 100, "fizz", "buzz");
            
            _fizzBuzzServiceMock.Setup(s => s.GenerateFizzBuzzAsync(3, 5, 100, "fizz", "buzz"))
                .ReturnsAsync(entity);
            
            _statisticsServiceMock.Setup(s => s.RecordRequestAsync(It.IsAny<FizzBuzzEntity>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Generate(requestDto);

            // Assert
            result.Should().BeOfType<ActionResult<FizzBuzzResponseDto>>();
            var actionResult = result as ActionResult<FizzBuzzResponseDto>;
            actionResult!.Result.Should().BeOfType<OkObjectResult>();
            
            var okResult = actionResult.Result as OkObjectResult;
            var response = okResult!.Value as FizzBuzzResponseDto;
            response!.Result.Should().Equal(entity.Result);
        }

        [Fact]
        public async Task Generate_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var requestDto = new FizzBuzzRequestDto
            {
                Int1 = 0, // Invalid
                Int2 = 5,
                Limit = 100,
                Str1 = "fizz",
                Str2 = "buzz"
            };

            _fizzBuzzServiceMock.Setup(s => s.GenerateFizzBuzzAsync(0, 5, 100, "fizz", "buzz"))
                .ThrowsAsync(new DomainException("int1 must be greater than 0"));

            // Act
            var result = await _controller.Generate(requestDto);

            // Assert
            result.Should().BeOfType<ActionResult<FizzBuzzResponseDto>>();
            var actionResult = result as ActionResult<FizzBuzzResponseDto>;
            actionResult!.Result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = actionResult.Result as BadRequestObjectResult;
            badRequestResult!.Value.Should().Be("int1 must be greater than 0");
        }
    }
}