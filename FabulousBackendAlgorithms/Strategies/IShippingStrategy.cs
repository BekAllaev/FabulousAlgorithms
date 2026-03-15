namespace FabulousBackendAlgorithms.Strategies
{
    public interface IShippingStrategy
    {
        string ProviderName { get; }
        decimal CalculateCost(int orderId);
    }
}
