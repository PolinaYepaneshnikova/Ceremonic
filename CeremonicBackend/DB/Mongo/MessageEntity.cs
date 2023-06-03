using System;

using MongoDB.Bson.Serialization.Attributes;

using CeremonicBackend.DB.Relational;

namespace CeremonicBackend.DB.Mongo
{
    public class MessageEntity : BaseEntity<int>
    {
        public int AuthorId { get; set; }
        public string Text { get; set; }
        public string ImageFileName { get; set; }
        public string FileName { get; set; }
        public DateTime PostedAt { get; set; }

        [BsonIgnoreIfNull]
        public bool? NotViewed { get; set; }
    }
}
