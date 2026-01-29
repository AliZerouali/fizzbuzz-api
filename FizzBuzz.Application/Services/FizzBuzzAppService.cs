using FizzBuzz.Application.Interfaces;
using FizzBuzz.Domain.Entities;

namespace FizzBuzz.Application.Services
{
    public class FizzBuzzAppService : IFizzBuzzService
    {
        public Task<FizzBuzzEntity> GenerateFizzBuzzAsync(int int1, int int2, int limit, string str1, string str2)
        {
            var entity = FizzBuzzEntity.Create(int1, int2, limit, str1, str2);
            return Task.FromResult(entity);
        }
    }
}