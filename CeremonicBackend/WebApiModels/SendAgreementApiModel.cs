using System;

namespace CeremonicBackend.WebApiModels
{
    public class SendAgreementApiModel
    {
        public int ClientId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string Service { get; set; }
    }
}
