using System.Collections.Generic;

namespace CeremonicBackend.DB.Mongo
{
    public class GuestMapEntity
    {
        public List<PersonEntity> Guests { get; set; }
        public string MapFileName { get; set; }
    }
}
