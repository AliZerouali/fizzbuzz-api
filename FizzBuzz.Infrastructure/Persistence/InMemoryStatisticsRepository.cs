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
        private readonly ConcurrentDictionary<string, int> _requestCounts = new();
        private readonly ReaderWriterLockSlim _lock = new();

        public Task IncrementRequestCountAsync(string key)
        {
            _requestCounts.AddOrUpdate(key, 1, (_, oldValue) => oldValue + 1);
            return Task.CompletedTask;
        }

        public Task<(string key, int count)> GetMostFrequentRequestKeyAsync()
        {
            if (_requestCounts.IsEmpty)
                return Task.FromResult((string.Empty, 0));

            var mostFrequent = _requestCounts.OrderByDescending(x => x.Value).First();
            return Task.FromResult((mostFrequent.Key, mostFrequent.Value));
        }
    }
}