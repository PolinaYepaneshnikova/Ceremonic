using CeremonicBackend.DB.Relational;
using System;

namespace CeremonicBackend.DB.Mongo
{
    public class WeddingEntity : JoinedToUserEntity
    {
        public PersonEntity User1 { get; set; }
        public PersonEntity User2 { get; set; }
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
