using Microsoft.AspNetCore.Components.Forms;

namespace HiddenVillaServer.Service;

public interface IFileUpload
{
    Task<string> UploadFile(IBrowserFile file);
    bool DeleteFile(string FileName);
    
}