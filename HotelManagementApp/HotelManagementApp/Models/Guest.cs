using System.ComponentModel.DataAnnotations;

namespace HotelManagementApp.Models
{
    public class Guest
    {
        [Key]
        public int guestId { get; set; }
        [Required]

        public string guestName { get; set; } = null!;

        public string guestEmail { get; set; } = null!;

        public int guestAge { get; set; }

        public long guestContact { get; set; }

        public long guestAadharId { get; set; }

        public string guestAddress { get; set; } = null!;

        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
