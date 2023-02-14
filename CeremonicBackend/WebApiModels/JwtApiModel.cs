namespace CeremonicBackend.WebApiModels
{
    public class JwtApiModel
    {
        public string JwtString { get; set; }
        public JwtApiModel(string jwtString)
        {
            JwtString = jwtString;
        }
    }
}
