using System.Globalization;
using HiddenVillaServer;
using HiddenVillaServer.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiddenVilla_Web_Api.Controllers
{
    [Route("api/[controller]")]
    public class HotelRoomController : Controller
    {
        private readonly IHotelRoomRepo _roomRepo;
        public HotelRoomController(IHotelRoomRepo roomRepo)
        {
            _roomRepo = roomRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotelRooms(string checkinDate=null,string CheckoutDate=null)
        {
            if (string.IsNullOrEmpty(checkinDate) || string.IsNullOrEmpty(CheckoutDate) )
            {
                return BadRequest(
                    new ErroModel()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Title = "ALL PARAMETERS MUST BE SUPPLIED"
                    });
            }

            if (!DateTime.TryParseExact(checkinDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out var dtcheckinDate))
            {
                return BadRequest(
                    new ErroModel()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Title = "invalid checkin date format try MM/dd/yyyy"
                    });
            }
            if (!DateTime.TryParseExact(CheckoutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out var dtCheckoutDate))
            {
                return BadRequest(
                    new ErroModel()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Title = "Invalid Checkin Date Format Try MM/dd/yyyy"
                    });
            }
            var allRooms =await _roomRepo.GetAllHotelRooms(checkinDate,CheckoutDate);
            return Ok(allRooms);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetHotelRoom(int? roomId,string checkinDate=null,string CheckoutDate=null)
        {
           if (roomId == null)
            {
                return BadRequest(new ErroModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Room Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
           if (string.IsNullOrEmpty(checkinDate) || string.IsNullOrEmpty(CheckoutDate) )
           {
               return BadRequest(
                   new ErroModel()
                   {
                       StatusCode = StatusCodes.Status400BadRequest,
                       Title = "Must Contains At Least  A Valid Date"
                   });
           }

           if (!DateTime.TryParseExact(checkinDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                   out var dtcheckinDate))
           {
               return BadRequest(
                   new ErroModel()
                   {
                       StatusCode = StatusCodes.Status400BadRequest,
                       Title = "invalid checkin date format try MM/dd/yyyy"
                   });
           }   
           
           if (!DateTime.TryParseExact(checkinDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                   out var dtcheckoutDate))
           {
               return BadRequest(
                   new ErroModel()
                   {
                       StatusCode = StatusCodes.Status400BadRequest,
                       Title = "invalid checkin date format try MM/dd/yyyy"
                   });
           }
            var roomDetails = await _roomRepo.GetHotelRoom(roomId.Value,checkinDate,CheckoutDate);
            if (roomDetails == null)
            {
                return BadRequest(new ErroModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Room Details",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(roomDetails);

        }
    }
}
