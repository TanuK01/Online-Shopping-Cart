using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace HotelManagementApp.Models
{
    public class Payment
    {
        [Key]
        public int paymentId { get; set; }
        [Required]

        public long paymentCard { get; set; }

        public string paymentCardHolder { get; set; } = null!;
    }
}
