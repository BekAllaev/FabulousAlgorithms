using FabulousBackendAlgorithms.Services;
using FabulousBackendAlgorithms.Strategies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddTransient<IShippingStrategy, ShippingStrategyA>();
builder.Services.AddTransient<IShippingStrategy, ShippingStrategyB>();

var currencyApiConfig = builder.Configuration.GetSection("CurrencyApi");
var baseUrl = currencyApiConfig["BaseUrl"] ??
              throw new InvalidOperationException("CurrencyApi:BaseUrl is required in configuration");
var apiKey = currencyApiConfig["ApiKey"] ??
             throw new InvalidOperationException("CurrencyApi:ApiKey is required in configuration");

builder.Services.AddHttpClient<CurrencyApiClient>(client =>
{
    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Add("apikey", apiKey);
});

builder.Services.AddScoped<ICurrencyApiClient, CurrencyApiClient>();
builder.Services.AddSingleton<ICurrencyLockProvider, CurrencyLockProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
