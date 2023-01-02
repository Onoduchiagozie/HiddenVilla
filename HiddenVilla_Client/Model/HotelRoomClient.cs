using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiddenVilla_Client.Model
{
    [NotMapped]
    public class HotelRoomClient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int Id { get; set; }
        [Required(ErrorMessage ="Enter A Room")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter An Occupancy")]
        public int Occupancy { get; set; }
        [Range(1,3000,ErrorMessage ="Input Is Out Of Specified Range")]
        public double RegularRate { get; set; }
        public string Details { get; set; }
        public string SqFt { get; set; }

        public double TotalDays { get; set; }
        public double TotalAmount { get; set; }
       
        public virtual ICollection<HotelClientImage>? HotelImages { get; set; }
        public List<ImageURI>? ImageUrls { get; set; }
        public bool IsBooked { get; set; }

    }
}
