using System;

using CeremonicBackend.DB.Relational;

namespace CeremonicBackend.WebApiModels
{
    public class AgreementApiModel
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public int ClientId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string Service { get; set; }
        public ConfirmStatus ConfirmStatus { get; set; }
    }
}
