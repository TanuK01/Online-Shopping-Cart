using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementApp.Models
{
    public class Bill
    {
        [Key]
        public int billId { get; set; }
        [Required]

        public double billAmount { get; set; }

        public DateTime billDate { get; set; }

        public virtual ICollection<Payment>? Payments { get; set; }

        public virtual ICollection<Room>? Rooms { get; set; }
    }
}
