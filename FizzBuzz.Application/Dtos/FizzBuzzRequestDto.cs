namespace FizzBuzz.Application.Dtos
{
    public class FizzBuzzRequestDto
    {
        public int Int1 { get; set; }
        public int Int2 { get; set; }
        public int Limit { get; set; }
        public string Str1 { get; set; } = string.Empty;
        public string Str2 { get; set; } = string.Empty;
    }

    public class FizzBuzzResponseDto
    {
        public List<string> Result { get; set; } = new List<string>();
    }

    public class StatisticsResponseDto
    {
        public FizzBuzzRequestDto? MostFrequentRequest { get; set; }
        public int HitCount { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}