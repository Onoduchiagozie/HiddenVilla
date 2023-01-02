using HiddenVilla_Client.Model;


using HiddenVilla_Client.Model.api_models;
namespace HiddenVilla_Client.Service.Repo
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO user);
        Task<AuthResponseDTO> Login(AuthenticationDTO authentication);
        Task Logout();

    }
}
