﻿using CeremonicBackend.DB.Mongo;

namespace CeremonicBackend.WebApiModels
{
    public class EditProviderApiModel
    {
        public int UserId { get; set; }
        public string Info { get; set; }
        public string Geolocation { get; set; }
        public string City { get; set; }
        public RangeEntity AveragePrice { get; set; }
    }
}
