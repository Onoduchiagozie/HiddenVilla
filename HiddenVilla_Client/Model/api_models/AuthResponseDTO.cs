namespace HiddenVilla_Client.Model.api_models;

public class AuthResponseDTO
{
    public bool IsAuthSuccessful { get; set; }
    public string Errors { get; set; }
    public string Token { get; set; }
    public UserDTO UserDto { get; set; }
}