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
                var roomOrder = details;
                roomOrder.OrderStatus = "Status Pending";
                var result=await _db.RoomOrderingDetails.AddAsync(roomOrder);

                await _db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex) 
            {
                throw ex;
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





        public async Task<RoomOrderDetails> PayMentSuccessful(int id)
        {
             
            var dataDb= await _db.RoomOrderingDetails.FindAsync(id);
            if(dataDb == null)
            {
                return null;
            }
            if (!dataDb.isPaymentSuccessful)
            {
                dataDb.isPaymentSuccessful = true;
                dataDb.OrderStatus = "Booked";
                var markPaySucess = _db.RoomOrderingDetails.Update(dataDb);
                await _db.SaveChangesAsync();
                return markPaySucess.Entity;
            }return new RoomOrderDetails();
        }

        public Task<bool> UpdateOrderStatus(int roomOrderId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
