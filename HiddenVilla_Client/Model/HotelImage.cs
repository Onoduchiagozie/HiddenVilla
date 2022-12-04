using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiddenVilla_Client.Model;

public class HotelImage
{
    [Key]
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string ImageUrl { get; set; }
    [ForeignKey("RoomId")] 
    public virtual HotelRoomDTO HotelRoom { get; set; }
    
}