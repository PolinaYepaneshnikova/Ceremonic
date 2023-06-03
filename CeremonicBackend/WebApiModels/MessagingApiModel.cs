using System.Collections.Generic;

namespace CeremonicBackend.WebApiModels
{
    public class MessagingApiModel
    {
        public int User1Id { get; set; }
        public int User2Id { get; set; }
        public List<MessageApiModel> MessagesList { get; set; }
    }
}
