using System.Collections.Generic;

using CeremonicBackend.DB.Mongo;

namespace CeremonicBackend.WebApiModels
{
    public class ProviderApiModel
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ServiceName { get; set; }
        public string BrandName { get; set; }
        public string AvatarFileName { get; set; }
        public List<string> ImageFileNames { get; set; }
        public string Info { get; set; }
        public string PlaceName { get; set; }
        public string Geolocation { get; set; }
        public string City { get; set; }
        public RangeEntity AveragePrice { get; set; }
        public object WorkingDayList { get; set; }
    }
}
