namespace HotelManagementApp.Models
{
    public class UpdateRoom
    {
        public bool roomStatus { get; set; }

        public string roomComment { get; set; } = null;

        public string roomInventory { get; set; } = null!;

        public float roomPrice { get; set; }
    }
}
