using FizzBuzz.Domain.Entities;

namespace FizzBuzz.Application.Interfaces
{
    public interface IFizzBuzzService
    {
        Task<FizzBuzzEntity> GenerateFizzBuzzAsync(int int1, int int2, int limit, string str1, string str2);
    }
}