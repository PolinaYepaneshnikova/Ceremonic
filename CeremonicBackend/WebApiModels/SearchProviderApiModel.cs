using System.Collections.Generic;

namespace CeremonicBackend.WebApiModels
{
    public class SearchProviderApiModel
    {
        public string keywords { get; set; }
        public IList<string> serviceNames { get; set; }
        public string date { get; set; }
        public string city { get; set; }
        public int? numberOfPriceCategory { get; set; }
        public int? numberOfGuestCountCategory { get; set; }
    }
}