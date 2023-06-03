using MongoDB.Bson;

using CeremonicBackend.DB.Relational;


namespace CeremonicBackend.DB.Mongo
{
    public class PersonEntity : BaseEntity<ObjectId>
    {
        public string FullName { get; set; }
        public string AvatarFileName { get; set; }
        public string Email { get; set; }
        public int PlusGuests { get; set; }
        public int CategoryId { get; set; }
        public bool WillCome { get; set; }
        public string Notes { get; set; }
    }
}
