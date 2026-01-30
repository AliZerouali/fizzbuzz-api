using System.Collections.Concurrent;

namespace FizzBuzz.Infrastructure.Persistence
{
    public interface IStatisticsRepository
    {
        Task IncrementRequestCountAsync(string key);

        Task<(string key, int count)> GetMostFrequentRequestKeyAsync();
    }

    public class InMemoryStatisticsRepository : IStatisticsRepository
    {
        private readonly ConcurrentDictionary<string, (int Count, DateTime LastUpdated)> _requestCounts = new();

        public Task IncrementRequestCountAsync(string key)
        {
            var now = DateTime.UtcNow;
            _requestCounts.AddOrUpdate(
                key,
                k => (1, now),
                (k, old) => (old.Count + 1, now)
            );

            Console.WriteLine("Toutes les entrées du cache:");
            foreach (var entry in _requestCounts)
            {
                Console.WriteLine($"   {entry.Key} => {entry.Value.Count} (last: {entry.Value.LastUpdated:o})");
            }

            return Task.CompletedTask;
        }

        public Task<(string key, int count)> GetMostFrequentRequestKeyAsync()
        {
            if (_requestCounts.IsEmpty)
                return Task.FromResult((string.Empty, 0));

            var mostFrequent = _requestCounts
                .OrderByDescending(x => x.Value.LastUpdated)
                .First();

            return Task.FromResult((mostFrequent.Key, mostFrequent.Value.Count));
        }
    }
}