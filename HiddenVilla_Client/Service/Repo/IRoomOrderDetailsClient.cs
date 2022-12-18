using HiddenVilla_Client.Model;

namespace HiddenVilla_Client.Service.Repo
{
    public interface IRoomOrderDetailsClient
    {
        public Task<RoomOrderDetails> SaveRoomOrderDetails(RoomOrderDetails details);
        public Task<RoomOrderDetails> PaymentSuccessfull(RoomOrderDetails details);
    }
}
