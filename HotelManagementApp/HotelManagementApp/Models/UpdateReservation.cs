namespace HotelManagementApp.Models
{
    public class UpdateReservation
    {
        public DateTime reservationCheckIn { get; set; }

        public DateTime reservationCheckOut { get; set; }

        public int reservationNoofGuests { get; set; }

        public virtual ICollection<Bill>? Bills { get; set; }
    }
}
