using HiddenVilla_Client.Model;
using HiddenVillaServer.Data.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace HiddenVilla_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomOrderController : Controller
    {
        private readonly IRoomOrderDetailsRepo _roomOrderDetailsRepo;
        public RoomOrderController(IRoomOrderDetailsRepo roomOrderDetailsRepo)
        {
            _roomOrderDetailsRepo = roomOrderDetailsRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoomOrderDetails details)
        {
          
                if (ModelState.IsValid)
                {
                    var result = await _roomOrderDetailsRepo.Create(details);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ErroModel
                    {
                        ErrorMessage = "Errooor while creating Room Details Booking"
                    });
                }
          
           

        }

        [HttpPost]
        public async Task<IActionResult> Paymentsuccessfull(RoomOrderDetails details)
        {
            var service = new SessionService();
            var currentSession = service.Get(details.StripeSessionId);
            if(currentSession.PaymentStatus == "paid")
            {
                var request = await _roomOrderDetailsRepo.PayMentSuccessful(details.Id);
                if(request == null)
                {
                    return BadRequest(new ErroModel(){ErrorMessage="Repo Service Could Not Mark Payment As Successfull"});
                }return Ok(request);

            }
            else
            {
                return BadRequest(new ErroModel()
                {
                    ErrorMessage="Could Not Mark Payment As Successfull"
                });
            }
        }




    }
}
