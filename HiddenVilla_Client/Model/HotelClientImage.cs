using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiddenVilla_Client.Model;

public class HotelClientImage
{
    [Key]
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string ImageUrl { get; set; }
    [ForeignKey("RoomId")] 
    public virtual HotelRoomClient HotelRoom { get; set; }
    
}