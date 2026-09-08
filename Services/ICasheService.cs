namespace MySurveyBasket.Services
{
    public interface ICasheService
    {
        Task<T?> GetAsync<T>(string key,CancellationToken cancellationToken =default)where T:class;
        Task SetAsync<T>(string key,T value, TimeSpan timeSpan , CancellationToken cancellationToken = default) where T : class;
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    }
}
