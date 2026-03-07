namespace FabulousBackendAlgorithms.Models
{
    public record ExchangeRateResponse(
        string Currency,
        string BaseCurrency,
        decimal Rate,
        decimal Amount,
        decimal ConvertedAmount);
}
