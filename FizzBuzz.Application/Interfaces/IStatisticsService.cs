using FizzBuzz.Domain.Entities;
using FizzBuzz.Domain.ValueObjects;

namespace FizzBuzz.Application.Interfaces
{
    public interface IStatisticsService
    {
        Task<Statistics> GetMostFrequentRequestAsync();
        Task RecordRequestAsync(FizzBuzzEntity request);
    }
}