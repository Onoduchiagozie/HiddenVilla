namespace HiddenVilla_Client.Model
{
    public class StripePayment
    {
        public string BoughtRoomName { get; set; }
        public long Amount { get; set;}
        public string? ImageUrl { get; set;}
        public string ReturnUrl { get; set;}
    }
}
