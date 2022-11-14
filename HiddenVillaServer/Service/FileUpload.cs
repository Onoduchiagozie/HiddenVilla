using System.Net;
using Microsoft.AspNetCore.Components.Forms;

namespace HiddenVillaServer.Service;

public class FileUpload:IFileUpload
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileUpload(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    
    public async Task<string> UploadFile(IBrowserFile file)
    {
        try
        {
            FileInfo fileInfo = new FileInfo(file.Name);
            var filename = Guid.NewGuid().ToString()+fileInfo.Extension;
            var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\RoomImages";
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "RoomImages",filename);

            var memoryStream = new MemoryStream();
            
            await file.OpenReadStream(1024000).CopyToAsync(memoryStream);
            if (!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }

            await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                memoryStream.WriteTo(fs);
            }

            var fullpath = $"RoomImages/{filename}";
            return fullpath;
        }
        catch(Exception ex)
        {
            throw ex;
        }
        
    }

    public bool DeleteFile(string FileName)
    {
        try
        {
            var path = $"{_webHostEnvironment.WebRootPath}\\RoomImages\\{FileName}";
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}