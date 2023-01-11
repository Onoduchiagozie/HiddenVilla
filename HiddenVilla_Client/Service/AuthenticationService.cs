using Blazored.LocalStorage;
using HiddenVilla_Client.Model.api_models;
using HiddenVilla_Client.Service.Repo;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;

namespace HiddenVilla_Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authStateProvider;


        public AuthenticationService(HttpClient httpClient, 
            ILocalStorageService localStorageService,
            AuthenticationStateProvider authStateProvider)
        {
            _httpClient= httpClient;
            _localStorageService= localStorageService;

            _authStateProvider = authStateProvider;
        }
        public async Task<AuthResponseDTO> Login(AuthenticationDTO authentication)
        {
            //SERIALIZE ENCODE SEND READPOST/RESPONE DESERIALIZE RESPONSE
            var serlialized=JsonConvert.SerializeObject(authentication);
            var content= new StringContent(serlialized,Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("api/account/signin",content);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStorageService.SetItemAsync("jwt_token",result.Token);
                await _localStorageService.SetItemAsync("User_Details",result.UserDto);
                ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);

                _httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("bearer", result.Token);

                return new AuthResponseDTO { IsAuthSuccessful = true,Token=result.Token, };
            }
            else
            {
                return result;
            }

        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("jwt_token");
            await _localStorageService.RemoveItemAsync("User_Details");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;


        }

        public async Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO user)
        {
            var serlialized = JsonConvert.SerializeObject(user);
            var content = new StringContent(serlialized, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/signup", content);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var codeJesus = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<RegistrationResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
              return new RegistrationResponseDTO { IsRegisterSuccessfull = true };
            }
            else
            {
                return result;
            }
        }
    }
}
