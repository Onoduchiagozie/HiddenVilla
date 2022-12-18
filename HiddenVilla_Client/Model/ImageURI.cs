using System.ComponentModel.DataAnnotations;

namespace HiddenVilla_Client.Model
{
    public class ImageURI
    {
            [Key]
            public int Id { get; set; }
            public string Uri { get; set; } = null!;
    }
}
