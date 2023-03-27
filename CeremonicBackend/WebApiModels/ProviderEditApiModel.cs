using CeremonicBackend.DB.Mongo;

namespace CeremonicBackend.WebApiModels
{
    public class ProviderEditApiModel
    {
        public string Info { get; set; }
        public string Geolocation { get; set; }
        public string City { get; set; }
        public RangeEntity AveragePrice { get; set; }
    }
}
