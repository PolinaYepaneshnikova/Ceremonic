using CeremonicBackend.DB.Mongo;
using System;

namespace CeremonicBackend.WebApiModels
{
    public class EditWeddingApiModel
    {
        public string FullName1 { get; set; }
        public string FullName2 { get; set; }
        public DateTime Date { get; set; }
        public string Geolocation { get; set; }
        public string City { get; set; }
        public RangeEntity GuestCountRange { get; set; }
        public RangeEntity ApproximateBudget { get; set; }
    }
}
