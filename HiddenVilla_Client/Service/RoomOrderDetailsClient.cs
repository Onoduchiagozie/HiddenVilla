using HiddenVilla_Client.Model;
using HiddenVilla_Client.Service;
using HiddenVilla_Client.Service.Repo;
using Newtonsoft.Json;
using System.Text;

namespace HiddenVilla_Client.Service
{
    public class RoomOrderDetailsClient : IRoomOrderDetailsClient
    {
        private HttpClient _httpClient;
        public RoomOrderDetailsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RoomOrderDetails> PaymentSuccessfull(RoomOrderDetails details)
        {

            var content = JsonConvert.SerializeObject(details);
            var bodycontent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("api/RoomOrder/paymentsuccessfull", bodycontent);
            string codejesus3 = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                var contentResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RoomOrderDetails>(contentResult);
                return result;
            }
            else
            {
                var conent = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(conent);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<RoomOrderDetails> SaveRoomOrderDetails(RoomOrderDetails details)
        {
            var content = JsonConvert.SerializeObject(details);
            var bodycontent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("api/RoomOrder/create", bodycontent);
            string codejesus3 = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                var contentResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RoomOrderDetails>(contentResult);
                return result;
            }
            else
            {
                var conent = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(conent);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}
