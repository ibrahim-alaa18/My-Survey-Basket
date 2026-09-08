
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MySurveyBasket.Services
{
    public class CasheService(IDistributedCache distributedCashe) : ICasheService
    {
        private readonly IDistributedCache _distributedCashe = distributedCashe;

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            var cashedValue =  await _distributedCashe.GetStringAsync(key, cancellationToken);
            if (string.IsNullOrEmpty(cashedValue))
                return null;
            else
                return JsonSerializer.Deserialize<T>(cashedValue);

        }
        public async Task SetAsync<T>(string key, T value, TimeSpan timeSpan ,CancellationToken cancellationToken = default) where T : class
        {
            await _distributedCashe.SetStringAsync(
                key, 
                JsonSerializer.Serialize(value),
                 new DistributedCacheEntryOptions
                 {
                     AbsoluteExpirationRelativeToNow = timeSpan
                 },
                cancellationToken);
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _distributedCashe.RemoveAsync(key, cancellationToken);
        }

      
    }
}
