using CeremonicBackend.DB.Mongo;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CeremonicBackend.WebApiModels
{
    public class ProviderEditApiModel
    {
        public string Info { get; set; }
        public string Geolocation { get; set; }
        public string City { get; set; }
        public RangeEntity AveragePrice { get; set; }
        public IList<string> DeletedImageNames { get; set; }
        public IList<IFormFile> AddedImageFiles { get; set; }
    }
}
