using Stripe.Checkout;
namespace TechXpress.Business.Services
{

    public interface IStripeService
    {
        Session CreateCheckoutSession(string successUrl, string cancelUrl, List<Stripe.Checkout.SessionLineItemOptions> lineItems);
    }
}
