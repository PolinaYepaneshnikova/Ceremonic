using System.ComponentModel.DataAnnotations.Schema;

namespace CeremonicBackend.DB.Relational
{
    public class UserLoginInfoEntity : BaseEntity<int>
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        [ForeignKey("Id")]
        public virtual UserEntity User { get; set; }
    }
}
