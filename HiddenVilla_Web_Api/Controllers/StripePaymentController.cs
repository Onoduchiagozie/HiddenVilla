using HiddenVilla_Client.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace HiddenVilla_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StripePaymentController : Controller
    {
        private readonly IConfiguration _configuration;

        public StripePaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        public async Task<IActionResult> Create(StripePayment stripePayment)
        {
            try
            {
                var domain = _configuration.GetValue<string>("HiddenVilla_Client_URL");
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                        "card"
                    },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = stripePayment.Amount,
                                Currency = "GBP",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = stripePayment.BoughtRoomName
                                },
                            },
                            Quantity = 1,

                        }
                    },
                    Mode = "payment",
                    SuccessUrl = domain + "/success-payment?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = domain + stripePayment.ReturnUrl,
                };
                var service = new  SessionService();
                Session session= await service.CreateAsync(options);
                return Ok( new SuccessModel
                {
                    Data=session.Id
                });
            }
            catch (Exception ex)
            {
                return BadRequest( new ErroModel { ErrorMessage=ex.Message } );

            }
           
        }
    }
}
