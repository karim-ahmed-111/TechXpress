// TechXpress.API/Controllers/Customer/PaymentController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechXpress.Business.Services;
using Stripe.Checkout;
using System.Collections.Generic;

namespace TechXpress.API.Controllers.Customer
{
    [ApiController]
    [Route("api/customer/[controller]")]
    [Authorize(Roles = "Customer")]
    public class PaymentController : ControllerBase
    {
        private readonly IStripeService _stripeService;
        public PaymentController(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpPost("create-checkout-session")]
        public IActionResult CreateCheckoutSession([FromBody] List<SessionLineItemOptions> items)
        {
            // items: list of line items (should come from cart or client)
            string successUrl = $"{Request.Scheme}://{Request.Host}/success";
            string cancelUrl = $"{Request.Scheme}://{Request.Host}/cancel";
            var session = _stripeService.CreateCheckoutSession(successUrl, cancelUrl, items);
            return Ok(new { SessionId = session.Id });
        }
    }
}
