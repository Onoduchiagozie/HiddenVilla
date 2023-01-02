using HiddenVilla_Client.Model;
using HiddenVilla_Client.Service.Repo;
using Newtonsoft.Json;
using System.Text;

namespace HiddenVilla_Client.Service
{
    public class StripePaymentService: IStripePaymentService
    {
        private readonly HttpClient _httpClient;
        public StripePaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<SuccessModel> Checkout(StripePayment payment)
        {
            var content= JsonConvert.SerializeObject(payment);
            var bodycontent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/stripepayment/create", bodycontent);
            var codeJesus = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                var contentResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SuccessModel>(contentResult);
                return result;
            }
            else
            {
                var conent = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(conent);
                return null;
            }
        }
    }
}
