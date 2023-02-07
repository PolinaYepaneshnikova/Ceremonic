using System.ComponentModel.DataAnnotations.Schema;

namespace CeremonicBackend.DB.Relational
{
    public class UserEntity : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ForeignKey("Id")]
        public virtual UserLoginInfoEntity LoginInfo { get; set; }
    }
}
