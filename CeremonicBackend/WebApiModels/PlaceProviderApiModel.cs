using CeremonicBackend.DB.Mongo;

namespace CeremonicBackend.WebApiModels
{
    public class PlaceProviderApiModel : ProviderApiModel
    {
        public RangeEntity GuestCount { get; set; }
    }
}
