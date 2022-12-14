using AutoMapper;
using HiddenVilla_Client.Pages.HotelRooms;
using HiddenVillaServer.Data.Repository.IRepository;
using HiddenVillaServer.Model;
using Microsoft.EntityFrameworkCore;

namespace HiddenVillaServer.Data.Repository
{
    public class HotelRoomRepo : IHotelRoomRepo
    {
        private readonly VillaDbContext _db;
        private readonly IMapper _mapper;
        public HotelRoomRepo(VillaDbContext db,IMapper mapper)
        {
           _db=db;
           _mapper=mapper;

        }


        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom hotelRoom= _mapper.Map<HotelRoomDTO,HotelRoom>(hotelRoomDTO);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "test";
            hotelRoom.UpdatedBy = "test";
            var addedRoom= await _db.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelRoom,HotelRoomDTO>(addedRoom.Entity);

        }

        public async Task<bool> IsRoomBooked(int roomId, string checkindatestr, string checkoutdatestr)
        {
            try
            {
                if(!string.IsNullOrEmpty(checkoutdatestr) && !string.IsNullOrEmpty(checkindatestr))
                {
                    var checkindate = DateTime.ParseExact(checkindatestr,"MM/dd/yyyy",null);
                    var checkoutdate = DateTime.ParseExact(checkoutdatestr,"MM/dd/yyyy", null);

                    var existingBooking = await _db.RoomOrderingDetails.Where(
                       x => x.RoomId == roomId && x.isPaymentSuccessful &&
                       ((checkindate < x.CheckOutDate && checkindate.Date >= x.CheckInDate)
                       || (checkoutdate.Date > x.CheckInDate.Date && checkindate.Date <= x.CheckInDate.Date)))
                        .FirstOrDefaultAsync();

                    if (existingBooking != null)
                    {
                        return true;
                    }
                    return false;

                }return true;
            }catch(Exception ex)
            {
                throw ex;
            }
              


        }


        public async Task<int> DeleteHotel(int roomId)
        {
            var roomDetails = await _db.HotelRooms.FindAsync(roomId);
            if (roomDetails != null)
            {
                var allimages = await _db.HotelImages.Where(x => x.RoomId == roomId).ToListAsync();
                _db.HotelImages.RemoveRange(allimages);
                
                _db.HotelRooms.Remove(roomDetails);
                return await _db.SaveChangesAsync();
            }
            else
            {
                return 0;
            }

        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms(string checkinDatestr,string CheckoutDatestr)
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRoomDTOs =
                    _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(
                        _db.HotelRooms.Include(x => x.HotelImages));

                if (!string.IsNullOrEmpty(CheckoutDatestr) && !string.IsNullOrEmpty(checkinDatestr))
                {
                    foreach(HotelRoomDTO hotelRoom in hotelRoomDTOs)
                    {
                        hotelRoom.IsBooked = await IsRoomBooked(hotelRoom.Id, checkinDatestr, CheckoutDatestr);
                    }
                }
                return hotelRoomDTOs;
                    
            }
            catch   (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId,string checkinDatestr,string CheckoutDatestr)
        {
            try
            {

                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(await _db.HotelRooms.Include(x => x.HotelImages)
                        .FirstOrDefaultAsync(x => x.Id == roomId)
                    );

                if (!string.IsNullOrEmpty(CheckoutDatestr) && !string.IsNullOrEmpty(checkinDatestr))
                {
                    hotelRoom.IsBooked = await IsRoomBooked(roomId, checkinDatestr, CheckoutDatestr);
                }
                return hotelRoom;

            }
            catch(Exception ex)
            {
                return null;
            }

        }

        public async Task<HotelRoomDTO> IsRoomUnique(string hotelname,int roomId=0)
        {
            try
            {
                if (roomId == 0)
                {
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                        await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == hotelname.ToLower())
                    );
                    return hotelRoom;
                }
                else
                {
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                        await _db.HotelRooms.FirstOrDefaultAsync(x =>
                            x.Name.ToLower() == hotelname.ToLower()
                            && x.Id != roomId)
                    );
                    return hotelRoom;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomId == hotelRoomDTO.Id)
                {
                    HotelRoom roomDetails=await _db.HotelRooms.FindAsync(roomId);
                    HotelRoom room= _mapper.Map<HotelRoomDTO,HotelRoom>(hotelRoomDTO,roomDetails);
                    room.UpdatedDate = DateTime.Now;
                    room.UpdatedBy = "test";
                    var updateroom=_db.HotelRooms.Update(room);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoom, HotelRoomDTO>(updateroom.Entity);

                }
                else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                return null;
            }
        }
    }
}
