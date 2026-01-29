using FizzBuzz.Application.Dtos;
using FizzBuzz.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzz.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("most-frequent")]
        public async Task<ActionResult<StatisticsResponseDto>> GetMostFrequentRequest()
        {
            var statistics = await _statisticsService.GetMostFrequentRequestAsync();

            if (statistics.MostFrequentRequest == null)
                return NotFound("No requests have been made yet.");

            var dto = new StatisticsResponseDto
            {
                MostFrequentRequest = new FizzBuzzRequestDto
                {
                    Int1 = statistics.MostFrequentRequest.Int1,
                    Int2 = statistics.MostFrequentRequest.Int2,
                    Limit = statistics.MostFrequentRequest.Limit,
                    Str1 = statistics.MostFrequentRequest.Str1,
                    Str2 = statistics.MostFrequentRequest.Str2
                },
                HitCount = statistics.HitCount,
                LastUpdated = statistics.LastUpdated
            };

            return Ok(dto);
        }
    }
}
