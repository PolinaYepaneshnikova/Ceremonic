using System.Collections.Generic;

using MongoDB.Bson;

using CeremonicBackend.DB.Relational;

namespace CeremonicBackend.DB.Mongo
{
    public class MessagingEntity : BaseEntity<ObjectId>
    {
        public int User1Id { get; set; }
        public int User2Id { get; set; }
        public List<MessageEntity> MessagesList { get; set; }
    }
}
