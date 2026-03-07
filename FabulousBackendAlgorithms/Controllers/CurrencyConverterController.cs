using FabulousBackendAlgorithms.Models;
using FabulousBackendAlgorithms.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace FabulousBackendAlgorithms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyConverterController(ICurrencyApiClient currencyClient, ICurrencyLockProvider lockProvider) : ControllerBase
    {
        private readonly ConcurrentDictionary<string, CacheEntry> cache = new();
        private readonly TimeSpan cacheDuration = TimeSpan.FromMinutes(10);

        public async Task<IResult> GetCurrencyRate(string currencyCode, decimal amount)
        {
            // Validate currency code format (3 uppercase letters)
            if (string.IsNullOrWhiteSpace(currencyCode) ||
                currencyCode.Length != 3 ||
                !currencyCode.All(char.IsLetter))
            {
                return Results.BadRequest(
                    new { error = "Currency code must be a 3-letter uppercase code (e.g., EUR, GBP)" });
            }

            // Validate amount (must be positive)
            if (amount < 0)
            {
                return Results.BadRequest(new { error = "Amount must be a positive number" });
            }

            var rate = await GetExchangeRate(currencyCode);

            if (rate == null)
            {
                return Results.NotFound(
                    new { error = $"Exchange rate for {currencyCode} not found or API error occurred" });
            }

            var convertedAmount = amount * rate.Value;

            return Results.Ok(new ExchangeRateResponse(
                Currency: currencyCode,
                BaseCurrency: "USD",
                Rate: rate.Value,
                Amount: amount,
                ConvertedAmount: convertedAmount
            ));
        }

        private async Task<decimal?> GetExchangeRate(string currencyCode)
        {
            // If we have a cached rate and it's still fresh, return it
            // Otherwise, fetch a new rate from the API and update the cache
            // This IsFresh check ensures we don't return stale data 
            if (cache.TryGetValue(currencyCode, out var cacheEntry) && IsFresh(cacheEntry))
            {
                return cacheEntry.Rate;
            }

            // Get the semaphore for this currency code. So we don't block requests for other
            // currencies while waiting for this one.
            // This allows multiple requests for different currencies to proceed in parallel,
            // while still ensuring that only one request for the same currency hits the
            // external API at a time.
            var semaphore = lockProvider.GetLock(currencyCode);

            // Wait up to 5 seconds to enter the semaphore.
            // If another request is currently inside, this call waits until it releases the semaphore.
            // If the timeout expires first, WaitAsync returns false.
            var acquired = await semaphore.WaitAsync(TimeSpan.FromSeconds(5));
            if (!acquired)
            {
                throw new TimeoutException("Could not acquire lock to fetch exchange rate. Please try again later.");
            }

            try
            {
                // Check again after acquiring the semaphore (double-check):
                // another request may have already fetched and cached the rate while we were waiting,
                // so this avoids an unnecessary external API call.
                if (cache.TryGetValue(currencyCode, out cacheEntry) && IsFresh(cacheEntry))
                {
                    return cacheEntry.Rate;
                }

                var currentRate = await currencyClient.GetExchangeRateAsync(currencyCode);

                if (currentRate.HasValue)
                {
                    cache.AddOrUpdate(
                        currencyCode,
                        _ => new CacheEntry(currentRate.Value, DateTime.UtcNow),
                        (_, _) => new CacheEntry(currentRate.Value, DateTime.UtcNow));
                }

                return currentRate;
            }
            finally
            {
                semaphore.Release();
            }
        }

        private record CacheEntry(decimal Rate, DateTime CreatedAtUtc);

        private bool IsFresh(CacheEntry entry) => DateTime.UtcNow - entry.CreatedAtUtc < cacheDuration;
    }
}
