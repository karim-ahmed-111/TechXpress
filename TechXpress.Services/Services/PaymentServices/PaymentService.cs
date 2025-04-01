// TechXpressApi.Services/Services/PaymentService.cs
using Microsoft.Extensions.Configuration;
using Stripe;

public class PaymentService : IPaymentService
{
    private readonly IConfiguration _config;

    public PaymentService(IConfiguration config)
    {
        _config = config;
        StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
    }

    public async Task<Charge> CreateChargeAsync(decimal amount, string token)
    {
        var options = new ChargeCreateOptions
        {
            Amount = (long)(amount * 100), // Convert to cents
            Currency = "usd",
            Source = token,
            Description = "TechXpress Order"
        };
        var service = new ChargeService();
        return await service.CreateAsync(options);
    }
}

