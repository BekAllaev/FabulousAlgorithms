namespace FabulousBackendAlgorithms.Strategies
{
    /// <summary>
    /// Example shipping strategy implementation for provider A.
    /// </summary>
    public class ShippingStrategyA : IShippingStrategy
    {
        /// <summary>
        /// Provider key used by clients to select this strategy.
        /// </summary>
        public string ProviderName => "StrategyA";

        /// <summary>
        /// Calculates shipping cost for provider A.
        /// </summary>
        /// <param name="orderId">Identifier of the order being processed.</param>
        /// <returns>Shipping cost for provider A.</returns>
        public decimal CalculateCost(int orderId)
        {
            return 1 + 2;
        }
    }
}
