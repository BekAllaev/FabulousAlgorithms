namespace FabulousBackendAlgorithms.Services
{
    public interface ICurrencyLockProvider
    {
        SemaphoreSlim GetLock(string currencyCode);
    }
}
