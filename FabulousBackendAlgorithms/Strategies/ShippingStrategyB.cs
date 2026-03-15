namespace FabulousBackendAlgorithms.Strategies
{
    public class ShippingStrategyB : IShippingStrategy
    {
        public string ProviderName => "StrategyB";
        public decimal CalculateCost(int orderId)
        {
            return 2 + 3;
        }
    }
}
