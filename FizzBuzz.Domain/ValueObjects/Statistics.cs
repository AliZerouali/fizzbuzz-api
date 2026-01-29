using FizzBuzz.Domain.Entities;

namespace FizzBuzz.Domain.ValueObjects
{
    public class Statistics
    {
        public FizzBuzzEntity? MostFrequentRequest { get; set; }
        public int HitCount { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}