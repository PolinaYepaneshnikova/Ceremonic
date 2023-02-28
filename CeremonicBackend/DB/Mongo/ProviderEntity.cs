using System.Collections.Generic;

namespace CeremonicBackend.DB.Mongo
{
    public class ProviderEntity : JoinedToUserEntity
    {
        public int ServiceId { get; set; }
        public string BrandName { get; set; }
        public string AvatarFileName { get; set; }
        public List<string> ImageFileNames { get; set; }
        public string PlaceName { get; set; }
        public string Geolocation { get; set; }
        public string City { get; set; }
        public RangeEntity AveragePrice { get; set; }
        public object WorkingDayList { get; set; }
    }
}
