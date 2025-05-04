// TechXpress.Business/Services/StripeService.cs
using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace TechXpress.Business.Services
{
    public class StripeService : IStripeService
    {
        private readonly StripeSettings _settings;

        public StripeService(IOptions<StripeSettings> stripeSettings)
        {
            _settings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _settings.SecretKey;
        }

        // Creates a Stripe Checkout Session given success/cancel URLs and line items
        public Session CreateCheckoutSession(string successUrl, string cancelUrl, List<SessionLineItemOptions> lineItems)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };
            var service = new SessionService();
            Session session = service.Create(options);
            return session;
        }
    }
}
