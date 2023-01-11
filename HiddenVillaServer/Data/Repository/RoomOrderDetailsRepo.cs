using HiddenVilla_Client.Model;
using HiddenVillaServer.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

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
                roomOrder.OrderStatus = "Pending";
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
                    _db.RoomOrderingDetails;

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
                RoomOrderDetails roomOrder = await _db.RoomOrderingDetails.FindAsync(roomOrderId);
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

        public async Task<bool> UpdateOrderStatus(int roomOrderId, string status)
        {
            try
            {
                var roomOrder = await _db.RoomOrderingDetails.FirstOrDefaultAsync(u => u.Id == roomOrderId);
                if(roomOrder == null)
                {
                    return false;
                }
                roomOrder.OrderStatus = status;
                if(status=="Checked In")
                {
                    roomOrder.ActualCheckInDate = DateTime.Now;
                }
                if (status == "Checked Out") 
                {
                    roomOrder.ActualCheckOutDate = DateTime.Now;
                }
                await _db.SaveChangesAsync(); 
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
