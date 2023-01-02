namespace HiddenVilla_Client.Model.api_models;

public class RegistrationResponseDTO
{
    public bool IsRegisterSuccessfull { get; set; }
    public IEnumerable<string> Errors { get; set; }
}