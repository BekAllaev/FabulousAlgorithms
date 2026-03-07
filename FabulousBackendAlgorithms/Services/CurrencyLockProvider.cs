using System.Collections.Concurrent;

namespace FabulousBackendAlgorithms.Services
{
    public class CurrencyLockProvider : ICurrencyLockProvider
    {
        // Stores one semaphore per currency code so requests for different currencies don't block each other.
        private readonly ConcurrentDictionary<string, SemaphoreSlim> locks = new();

        public SemaphoreSlim GetLock(string currencyCode)
        {
            // Normalize code so "eur" and "EUR" use the same semaphore.
            var normalizedCode = currencyCode.ToUpperInvariant();

            // Create a semaphore lazily if this currency has no lock yet.
            return locks.GetOrAdd(normalizedCode, _ => new SemaphoreSlim(1, 1));
        }
    }
}
