using FabulousBackendAlgorithms.Strategies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FabulousBackendAlgorithms.Controllers
{
    /// <summary>
    /// This controller is used for demonstration of Strategy pattern
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Dictionary<string, IShippingStrategy> _shippingStrategies;

        public OrderController(IEnumerable<IShippingStrategy> shippingStrategies)
        {
            _shippingStrategies = shippingStrategies.ToDictionary(x => x.ProviderName, x => x);
        }

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
