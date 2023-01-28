using CeremonicBackend.DB.Relational;
using System;

namespace CeremonicBackend.DB.NoSQL
{
    public class WeddingEntity : JoinedToUserEntity
    {
        public PersonEntity Wife { get; set; }
        public PersonEntity Husband { get; set; }
        public string Geolocation { get; set; }
        public DateTime Date { get; set; }
        public RangeEntity GuestCountRange { get; set; }
        public object GuestMap { get; set; }
        public object WeddingPlan { get; set; }
        public object[] WeddingTeam { get; set; }
        public RangeEntity ApproximateBudget { get; set; }
        public object Budget { get; set; }
    }
}
