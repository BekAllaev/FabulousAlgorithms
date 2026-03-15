namespace FabulousBackendAlgorithms.Strategies
{
    /// <summary>
    /// THIS INTERFACE IS FOR STRATEGY PATTERN DEMONSTRATION
    /// Defines a shipping cost calculation strategy for a specific provider.
    /// </summary>
    public interface IShippingStrategy
    {
        /// <summary>
        /// Gets the provider key used to resolve this strategy at runtime.
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// Calculates shipping cost for the specified order.
        /// </summary>
        /// <param name="orderId">Identifier of the order being processed.</param>
        /// <returns>Calculated shipping cost.</returns>
        decimal CalculateCost(int orderId);
    }
}
