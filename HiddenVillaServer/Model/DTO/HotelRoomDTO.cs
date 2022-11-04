using System.ComponentModel.DataAnnotations;

namespace HiddenVillaServer.Model
{
    public class HotelRoomDTO
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter A Room")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter An Occupancy")]
        public int Occupancy { get; set; }
        [Range(1,3000,ErrorMessage ="Input Is Out Of Specified Range")]
        public double RegularRate { get; set; }
        public string Details { get; set; }
        public string SqFt { get; set; }
       
    }
}
