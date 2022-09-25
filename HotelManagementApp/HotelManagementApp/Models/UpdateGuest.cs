namespace HotelManagementApp.Models
{
    public class UpdateGuest
    {
        public string guestName { get; set; } = null!;

        public string guestEmail { get; set; } = null!;

        public int guestAge { get; set; }

        public long guestContact { get; set; }

        public long guestAadharId { get; set; }

        public string guestAddress { get; set; } = null!;

        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
