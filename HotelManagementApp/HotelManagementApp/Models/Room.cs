using System.ComponentModel.DataAnnotations;

namespace HotelManagementApp.Models
{
    public class Room
    {
        [Key]
        public int roomId { get; set; }
        [Required]

        public bool roomStatus { get; set; }

        public string roomComment { get; set; } = null;

        public string roomInventory { get; set; } = null!;

        public float roomPrice { get; set; }
    }
}
