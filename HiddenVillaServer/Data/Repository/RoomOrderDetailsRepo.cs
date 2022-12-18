using HiddenVilla_Client.Model;
using HiddenVillaServer.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HiddenVillaServer.Data.Repository
{
    public class RoomOrderDetailsRepo : IRoomOrderDetailsRepo
    {
        private readonly VillaDbContext _db;
        public RoomOrderDetailsRepo(VillaDbContext db)
        {
            _db = db;
        }
        public async Task<RoomOrderDetails> Create(RoomOrderDetails details)
        {
            try
            {
                details.CheckInDate = details.CheckInDate.Date;
                details.CheckOutDate= details.CheckOutDate.Date;
                var roomOrder = details;
                roomOrder.OrderStatus = "Status Pending";
                var result=await _db.RoomOrderingDetails.AddAsync(roomOrder);
                await _db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex) 
            {
                return null;
            }

        }

        public  async Task<IEnumerable<RoomOrderDetails>> GetAllRoomOrderDetails()
        {
            try
            {
                IEnumerable<RoomOrderDetails> roomOrderDetails =
                    _db.RoomOrderingDetails.Include(u => u.hotelRoomDTO);

                return roomOrderDetails;

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<RoomOrderDetails> GetRoomOrderDetail(int roomOrderId)
        {
            try
            {
                RoomOrderDetails roomOrder = await _db.RoomOrderingDetails.Include(u => u.hotelRoomDTO)
                     .ThenInclude(x => x.HotelImages).FirstOrDefaultAsync(u => u.Id == roomOrderId);

                roomOrder.hotelRoomDTO.TotalDays = roomOrder.CheckOutDate.Subtract(roomOrder.CheckInDate).Days;

                return roomOrder;

            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<bool> IsRoomBooked(int roomId, DateTime checkindate, DateTime checkoutdate)
        {
           var status = false;
            var existingBooking = await _db.RoomOrderingDetails.Where(
                x => x.RoomId == roomId && x.isPaymentSuccessful &&
                (checkindate < x.CheckOutDate && checkindate.Date > x.CheckInDate
                || checkoutdate.Date > x.CheckInDate.Date && checkindate.Date < x.CheckInDate.Date)).FirstOrDefaultAsync();

            if (existingBooking != null)
            {
                status = true;
            }
            return status;

                                                                   
        }

        public Task<RoomOrderDetails> PayMentSuccessful(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrderStatus(int roomOrderId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
