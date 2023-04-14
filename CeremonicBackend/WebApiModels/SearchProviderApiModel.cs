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
        public string orderBy { get; set; }
    }

    public class OrderProvidersBy
    {
        public static readonly string None = "None",
                                      ByPriceAsc = "ByPriceAsc",
                                      ByPriceDesc = "ByPriceDesc",
                                      ByPriceCategoryAsc = "ByPriceCategoryAsc",
                                      ByPriceCategoryDesc = "ByPriceCategoryDesc";

        public static readonly string[] Values = new string[]
        {
            None, ByPriceAsc, ByPriceDesc, ByPriceCategoryAsc, ByPriceCategoryDesc,
        };
    }
}