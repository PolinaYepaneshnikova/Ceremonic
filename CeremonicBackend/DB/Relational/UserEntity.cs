namespace CeremonicBackend.DB.Relational
{
    public class UserEntity : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
