namespace HiddenVilla_Web_Api.Model;

public class AuthResponseDTO
{
    public bool IsAuthSuccessful { get; set; }
    public string Errors { get; set; }
    public string Token { get; set; }
    public UserDTO UserDto { get; set; }
}