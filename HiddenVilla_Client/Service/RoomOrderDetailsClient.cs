using HiddenVilla_Client.Model;
using HiddenVilla_Client.Service;
using HiddenVilla_Client.Service.Repo;

namespace HiddenVilla_Client.Service
{
    public class RoomOrderDetailsClient : IRoomOrderDetailsClient
    {
        private readonly HttpClient _httpClient;
        public RoomOrderDetailsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<RoomOrderDetails> PaymentSuccessfull(RoomOrderDetails details)
        {
            throw new NotImplementedException();
        }

        public Task<RoomOrderDetails> SaveRoomOrderDetails(RoomOrderDetails details)
        {
            throw new NotImplementedException();
        }
    }
}
