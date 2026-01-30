namespace FizzBuzz.Domain.Entities
{
    public class FizzBuzzEntity
    {
        public int Int1 { get; private set; }
        public int Int2 { get; private set; }
        public int Limit { get; private set; }
        public string Str1 { get; private set; }
        public string Str2 { get; private set; }
        public List<string> Result { get; private set; }

        private FizzBuzzEntity()
        { }

        public static FizzBuzzEntity Create(int int1, int int2, int limit, string str1, string str2)
        {
            Validate(int1, int2, limit, str1, str2);

            var entity = new FizzBuzzEntity
            {
                Int1 = int1,
                Int2 = int2,
                Limit = limit,
                Str1 = str1,
                Str2 = str2
            };

            entity.GenerateResult();
            return entity;
        }

        private static void Validate(int int1, int int2, int limit, string str1, string str2)
        {
            if (int1 <= 0) throw new DomainException("int1 must be greater than 0");
            if (int2 <= 0) throw new DomainException("int2 must be greater than 0");
            if (limit <= 0) throw new DomainException("limit must be greater than 0");
            if (string.IsNullOrWhiteSpace(str1)) throw new DomainException("str1 cannot be empty");
            if (string.IsNullOrWhiteSpace(str2)) throw new DomainException("str2 cannot be empty");
        }

        private void GenerateResult()
        {
            Result = new List<string>();

            for (int i = 1; i <= Limit; i++)
            {
                if (i % Int1 == 0 && i % Int2 == 0)
                {
                    Result.Add(Str1 + Str2);
                }
                else if (i % Int1 == 0)
                {
                    Result.Add(Str1);
                }
                else if (i % Int2 == 0)
                {
                    Result.Add(Str2);
                }
                else
                {
                    Result.Add(i.ToString());
                }
            }
        }

        public string GetCacheKey()
        {
            return $"{Int1}|{Int2}|{Limit}|{Str1?.ToLowerInvariant()}|{Str2?.ToLowerInvariant()}";
        }
    }

    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}