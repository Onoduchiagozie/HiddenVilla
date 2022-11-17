using System.ComponentModel.DataAnnotations;

namespace HiddenVillaServer.Model
{
    public class HotelAmenity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Timing { get; set; }
        [Required]
        public string Icon{ get; set; }
    }
}
