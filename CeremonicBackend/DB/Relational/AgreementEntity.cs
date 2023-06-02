using System;

namespace CeremonicBackend.DB.Relational
{
    public class AgreementEntity : BaseEntity<int>
    {
        public int ProviderId { get; set; }
        public int ClientId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string Service { get; set; }
        public ConfirmStatus ConfirmStatus { get; set; }
    }

    public enum ConfirmStatus
    {
        NoInfo = 0,
        Yes = 1,
        No = 2,
    }
}
