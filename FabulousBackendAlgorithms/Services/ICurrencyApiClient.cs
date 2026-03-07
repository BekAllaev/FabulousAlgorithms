namespace FabulousBackendAlgorithms.Services
{
    public interface ICurrencyApiClient
    {
        Task<decimal?> GetExchangeRateAsync(string currencyCode);
    }
}
