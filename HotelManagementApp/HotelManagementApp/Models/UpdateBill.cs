namespace HotelManagementApp.Models
{
    public class UpdateBill
    {
        public double billAmount { get; set; }

        public DateTime billDate { get; set; }

        public virtual ICollection<Payment>? Payments { get; set; }

        public virtual ICollection<Room>? Rooms { get; set; }
    }
}
