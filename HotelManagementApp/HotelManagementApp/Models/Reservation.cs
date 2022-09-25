using System.ComponentModel.DataAnnotations;

namespace HotelManagementApp.Models
{
    public class Reservation
    {
        [Key]
        public int reservationId { get; set; }
        [Required]

        public DateTime reservationCheckIn { get; set; }

        public DateTime reservationCheckOut { get; set; }

        public int reservationNoofGuests { get; set; }
        
        public virtual ICollection<Bill>? Bills { get; set; }
    }
}
