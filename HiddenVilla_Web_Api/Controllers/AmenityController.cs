using System.Globalization;
using HiddenVillaServer;
using HiddenVillaServer.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiddenVilla_Web_Api.Controllers
{
    [Route("api/[controller]")]
    public class AmenityController : Controller
    {
        private readonly IHotelAmenity _roomRepo;
        public AmenityController(IHotelAmenity roomRepo)
        {
            _roomRepo = roomRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAmenity()
        {

            var allAmenity = await _roomRepo.GetAllHotelAmenity();
            return Ok(allAmenity);
        }

        [HttpGet("{amenityId}")]
        public async Task<IActionResult> GetAmenity(int amenityId)
        {       
            var roomDetails = await _roomRepo.GetHotelAmenity(amenityId);
            return Ok(roomDetails);
        }
    }
}
