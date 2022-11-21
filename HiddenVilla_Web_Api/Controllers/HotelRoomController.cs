using HiddenVillaServer.Data.Repository.IRepository;
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
        public async Task<IActionResult> GetHotelRooms()
        {
            var allRooms =await _roomRepo.GetAllHotelRooms();
            return Ok(allRooms);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetHotelRoom(int? roomId)
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
            var roomDetails = await _roomRepo.GetHotelRoom(roomId.Value);
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
