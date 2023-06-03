using Microsoft.IdentityModel.Tokens;

using System;
using System.Text;

namespace CeremonicBackend.Authentification
{
    public class AuthOptions
    {
        public static string Issuer { get; set; }
        public static string Audience { get; set; }
        public static TimeSpan Lifetime { get; set; }
        public static string Key { get; set; }
        public static string JwtEmailEncryption { get; set; }
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
