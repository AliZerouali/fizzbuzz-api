using FizzBuzz.Application.Interfaces;
using FizzBuzz.Domain.Entities;
using FizzBuzz.Domain.ValueObjects;
using FizzBuzz.Infrastructure.Persistence;

namespace FizzBuzz.Infrastructure.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _repository;

        public StatisticsService(IStatisticsRepository repository)
        {
            _repository = repository;
        }

        public async Task RecordRequestAsync(FizzBuzzEntity request)
        {
            var cacheKey = request.GetCacheKey();
            Console.WriteLine($"Enregistrement de la requête avec clé: {cacheKey}");
            await _repository.IncrementRequestCountAsync(cacheKey);
        }

        public async Task<Statistics> GetMostFrequentRequestAsync()
        {
            var (key, count) = await _repository.GetMostFrequentRequestKeyAsync();

            Console.WriteLine($"Statistiques - Clé la plus fréquente: {key}, Count: {count}");

            if (string.IsNullOrEmpty(key))
                return new Statistics { HitCount = 0, LastUpdated = DateTime.UtcNow };

            // Parse key back to entity
            var parts = key.Split('|');
            var entity = FizzBuzzEntity.Create(
                int.Parse(parts[0]),
                int.Parse(parts[1]),
                int.Parse(parts[2]),
                parts[3],
                parts[4]
            );

            return new Statistics
            {
                MostFrequentRequest = entity,
                HitCount = count,
                LastUpdated = DateTime.UtcNow
            };
        }
    }
}