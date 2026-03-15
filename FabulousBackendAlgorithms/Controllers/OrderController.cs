using FabulousBackendAlgorithms.Strategies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FabulousBackendAlgorithms.Controllers
{
    /// <summary>
    /// Exposes endpoints for calculating shipping costs using the selected shipping strategy.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Dictionary<string, IShippingStrategy> _shippingStrategies;

        /// <summary>
        /// Builds a lookup of available shipping strategies keyed by provider name.
        /// </summary>
        /// <param name="shippingStrategies">All registered shipping strategy implementations.</param>
        public OrderController(IEnumerable<IShippingStrategy> shippingStrategies)
        {
            _shippingStrategies = shippingStrategies.ToDictionary(x => x.ProviderName, x => x);
        }

        /// <summary>
        /// Calculates shipping cost for an order using the requested shipping provider.
        /// </summary>
        /// <param name="orderId">Identifier of the order to calculate shipping for.</param>
        /// <param name="shippingProvider">Provider key (for example, <c>StrategyA</c> or <c>StrategyB</c>).</param>
        /// <returns>Calculated shipping cost or a bad request when the provider is unknown.</returns>
        [HttpGet]
        public ActionResult<decimal> GetShippingCost(int orderId, string shippingProvider)
        {
            if (!_shippingStrategies.TryGetValue(shippingProvider, out var strategy))
            {
                return BadRequest($"Shipping provider '{shippingProvider}' not found.");
            }

            return Ok(strategy.CalculateCost(orderId));
        }
    }
}
