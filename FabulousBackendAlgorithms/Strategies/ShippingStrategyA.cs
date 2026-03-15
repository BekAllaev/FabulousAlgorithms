namespace FabulousBackendAlgorithms.Strategies
{
    public class ShippingStrategyA : IShippingStrategy
    {
        public string ProviderName => "StrategyA";

        public decimal CalculateCost(int orderId)
        {
            return 1 + 2;
        }
    }
}
