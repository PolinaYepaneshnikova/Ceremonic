namespace CeremonicBackend.WebApiModels
{
    public class MessagingCardApiModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CountOfNotViewedMessages { get; set; }
    }
}
