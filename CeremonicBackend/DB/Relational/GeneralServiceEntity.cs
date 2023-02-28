namespace CeremonicBackend.DB.Relational
{
    public class GeneralServiceEntity : BaseEntity<int>
    {
        public Relational.ServiceEntity RelationalServiceEntity { get; set; }
        public Mongo.ServiceEntity MongoServiceEntity { get; set; }
    }
}
