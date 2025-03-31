// TechXpressApi.Services/Interfaces/IPaymentService.cs
using Stripe;

public interface IPaymentService
{
    Task<Charge> CreateChargeAsync(decimal amount, string token);
}