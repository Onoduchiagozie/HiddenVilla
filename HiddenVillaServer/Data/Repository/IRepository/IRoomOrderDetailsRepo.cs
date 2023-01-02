using HiddenVilla_Client.Model;

namespace HiddenVillaServer.Data.Repository.IRepository
{
    public interface IRoomOrderDetailsRepo
    {

        public Task<RoomOrderDetails> Create(RoomOrderDetails details);
        public Task<RoomOrderDetails> GetRoomOrderDetail(int roomOrderId);
        public Task<IEnumerable<RoomOrderDetails>> GetAllRoomOrderDetails();
        public Task<RoomOrderDetails> PayMentSuccessful(int id);
        public Task<bool> UpdateOrderStatus(int roomOrderId, string status);
    }
}
