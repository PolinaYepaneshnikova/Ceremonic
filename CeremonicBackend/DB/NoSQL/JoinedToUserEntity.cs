using MongoDB.Bson.Serialization.Attributes;

namespace CeremonicBackend.DB.NoSQL
{
    public abstract class JoinedToUserEntity
    {
        [BsonId]
        public int UserId { get; set; }
    }
}
