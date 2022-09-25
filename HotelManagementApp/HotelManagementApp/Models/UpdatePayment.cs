namespace HotelManagementApp.Models
{
    public class UpdatePayment
    {
        public long paymentCard { get; set; }

        public string paymentCardHolder { get; set; } = null!;
    }
}
