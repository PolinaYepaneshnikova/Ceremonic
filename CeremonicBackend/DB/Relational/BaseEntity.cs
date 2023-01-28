using MongoDB.Bson.Serialization.Attributes;

namespace CeremonicBackend.DB.Relational
{
    public abstract class BaseEntity<IdType>
    {
        [BsonId]
        public IdType Id { get; set; }
    }
}
