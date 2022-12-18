using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiddenVilla_Client.Model
{
    public class RoomOrderDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string StripeSessionId { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        public DateTime ActualCheckOutDate { get; set; }
        public DateTime ActualCheckInDate { get; set; }
        [Required]
        public long TotalCost { get; set; }
        public int RoomId { get; set; }
        public bool isPaymentSuccessful { get; set; } =false;
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [ForeignKey("RoomId")]
        public HotelRoomClient hotelRoomDTO { get; set; }
        public string OrderStatus { get; set; }

    }
}
