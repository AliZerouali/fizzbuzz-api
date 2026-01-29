using FizzBuzz.Application.Dtos;
using FizzBuzz.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzz.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FizzBuzzController : ControllerBase
    {
        private readonly IFizzBuzzService _fizzBuzzService;
        private readonly IStatisticsService _statisticsService;
        private readonly ILogger<FizzBuzzController> _logger;

        public FizzBuzzController(
            IFizzBuzzService fizzBuzzService,
            IStatisticsService statisticsService,
            ILogger<FizzBuzzController> logger)
        {
            _fizzBuzzService = fizzBuzzService;
            _statisticsService = statisticsService;
            _logger = logger;
        }

        [HttpGet("generate")]
        public async Task<ActionResult<FizzBuzzResponseDto>> Generate(
            [FromQuery] FizzBuzzRequestDto request)
        {
            try
            {
                var result = await _fizzBuzzService.GenerateFizzBuzzAsync(
                    request.Int1, request.Int2, request.Limit, request.Str1, request.Str2);

                await _statisticsService.RecordRequestAsync(result);

                return Ok(new FizzBuzzResponseDto { Result = result.Result });
            }
            catch (Domain.Entities.DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
