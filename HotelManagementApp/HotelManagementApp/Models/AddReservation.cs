namespace HotelManagementApp.Models
{
    public class AddReservation
    {
        public DateTime reservationCheckIn { get; set; }

        public DateTime reservationCheckOut { get; set; }

        public int reservationNoofGuests { get; set; }
    }
}
