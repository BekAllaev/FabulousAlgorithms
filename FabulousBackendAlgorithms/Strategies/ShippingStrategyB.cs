namespace FabulousBackendAlgorithms.Strategies
{
    /// <summary>
    /// Example shipping strategy implementation for provider B.
    /// </summary>
    public class ShippingStrategyB : IShippingStrategy
    {
        /// <summary>
        /// Provider key used by clients to select this strategy.
        /// </summary>
        public string ProviderName => "StrategyB";

        /// <summary>
        /// Calculates shipping cost for provider B.
        /// </summary>
        /// <param name="orderId">Identifier of the order being processed.</param>
        /// <returns>Shipping cost for provider B.</returns>
        public decimal CalculateCost(int orderId)
        {
            return 2 + 3;
        }
    }
}
