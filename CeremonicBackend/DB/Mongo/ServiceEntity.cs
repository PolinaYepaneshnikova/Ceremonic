using System.Collections.Generic;

using CeremonicBackend.DB.Relational;

namespace CeremonicBackend.DB.Mongo
{
    public class ServiceEntity : BaseEntity<int>
    {
        public List<RangeEntity> PriceRanges { get; set; }
        public List<RangeEntity> GuestCountRanges { get; set; }
    }
}
