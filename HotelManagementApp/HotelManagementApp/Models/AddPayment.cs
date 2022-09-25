namespace HotelManagementApp.Models
{
    public class AddPayment
    {
        public long paymentCard { get; set; }
        public string paymentCardHolder { get; set; } = null!;
    }
}
