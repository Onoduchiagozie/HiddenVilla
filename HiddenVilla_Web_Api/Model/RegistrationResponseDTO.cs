namespace HiddenVilla_Web_Api.Model;

public class RegistrationResponseDTO
{
    public bool IsRegisterSuccessfull { get; set; }
    public IEnumerable<string> Errors { get; set; }
}