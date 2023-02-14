using MongoDB.Bson.Serialization.Attributes;

namespace CeremonicBackend.DB.Mongo
{
    public abstract class JoinedToUserEntity
    {
        [BsonId]
        public int UserId { get; set; }
    }
}
