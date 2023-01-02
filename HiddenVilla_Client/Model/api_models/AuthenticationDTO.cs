using System.ComponentModel.DataAnnotations;

namespace HiddenVilla_Client.Model.api_models;

public class AuthenticationDTO
{
    [Required(ErrorMessage = "UserName is required")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
     
}