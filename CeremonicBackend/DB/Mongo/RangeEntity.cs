namespace CeremonicBackend.DB.Mongo
{
    public class RangeEntity
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }

        public RangeEntity()
        {
            Min = 0; Max = 0;
        }
        public RangeEntity(decimal min, decimal max)
        {
            Min = min; Max = max;
        }
    }
}
