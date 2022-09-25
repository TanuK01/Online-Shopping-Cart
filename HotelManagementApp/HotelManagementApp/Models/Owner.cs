using System.ComponentModel.DataAnnotations;

namespace HotelManagementApp.Models
{
    public class Owner
    {
        [Key]
        public int loginId { get; set; }

        public string loginPassword { get; set; } = null!;
    }
}
