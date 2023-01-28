namespace CeremonicBackend.DB.Relational
{
    public class UserLoginInfoEntity : BaseEntity<int>
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
